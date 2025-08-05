using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.JobMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;
using BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class JobUserMappingService(
    IJobUserMappingRepository jobUserMappingRepository,
    IUserRepository userRepository
) : IJobUserMappingService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterJobUserMappingViewModel>> FilterAsync(AdminFilterJobUserMappingViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<JobUserMapping>();
        var orders = Filter.GenerateOrders<JobUserMapping>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.JobTitle))
            conditions.Add(x => EF.Functions.Like(x.Job.Title, $"%{filter.JobTitle.Trim()}%"));

        if (filter.UserId > 0)
            conditions.Add(x => x.UserId == filter.UserId);

        switch (filter.JobExpirationStatus)
        {
            case JobExpirationStatus.Active:
                conditions.Add(x => x.Job.ExpireDate >= DateTime.Now);
                break;
            case JobExpirationStatus.All:
                break;
            case JobExpirationStatus.Expired:
                conditions.Add(x => x.Job.ExpireDate < DateTime.Now);
                break;
        }

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Descending:
                orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
                break;

            case FilterOrderBy.Ascending:
                orders.Add(x => x.CreatedDate);
                break;
        }

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;
        }

        #endregion

        string[] includes =
        [
            nameof(JobUserMapping.User),
            nameof(JobUserMapping.Job)
        ];


        await jobUserMappingRepository.FilterAsync(filter, conditions, x => x.MapToAdminJobUserMappingViewModel(), orders, includes: includes);
        return filter;
    }

    #endregion

    #region GetByIdAsync

    public async Task<Result<AdminJobUserMappingDetailsViewModel>> GetByIdAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminJobUserMappingDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(JobUserMapping.User),
            nameof(JobUserMapping.Job)
        ];

        var jobUserMapping = await jobUserMappingRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: includes);

        if (jobUserMapping is null)
            return Result.Failure<AdminJobUserMappingDetailsViewModel>(ErrorMessages.NotFoundError);

        return jobUserMapping.MapToAdminJobUserMappingDetailsViewModel();
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(int id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var blog = await jobUserMappingRepository.GetByIdAsync(id);

        if (blog is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = jobUserMappingRepository.SoftDeleteOrRecover(blog);
        await jobUserMappingRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #endregion

    #region Client

    #region CreateAsync

    public async Task<Result> CreateAsync(ClientCreateJobUserMappingViewModel model)
    {
        var jobUser = model.MapToJobUserMapping();

        #region Add File

        if (model.Attachment is not null)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(model.Attachment.FileName);

            var result = await model.Attachment.AddArchiveFilesToServer(fileName, FilePaths.ResumeAttachment);

            if (result.IsFailure)
                return Result.Failure(result.Message);

            jobUser.Attachment = result.Value;
        }

        #endregion

        var user = await userRepository.GetByIdAsync(model.UserId);

        if (user == null || user.Id < 1)
            return Result.Failure<ClientOrderDetailsViewModel>(ErrorMessages.BadRequestError);

        if (user.FirstName.IsNullOrEmptyOrWhiteSpace() || user.LastName.IsNullOrEmptyOrWhiteSpace())
            return Result.Failure(string.Format(ErrorMessages.RequiredUserFullName, "ثبت درخواست شغل"));

        if (await jobUserMappingRepository.AnyAsync(x => x.UserId == model.UserId && x.JobId == model.JobId))
            return Result.Failure(ErrorMessages.AlreadyJobSubmitError);


        await jobUserMappingRepository.InsertAsync(jobUser);
        await jobUserMappingRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    #endregion

    #region FilterAsync

    public async Task<Result<ClientFilterJobUserMappingViewModel>> FilterAsync(ClientFilterJobUserMappingViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<JobUserMapping>();
        var orders = Filter.GenerateOrders<JobUserMapping>();

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);

        conditions.Add(x => !x.IsDeleted);

        conditions.Add(x => x.UserId == filter.UserId);

        string[] includes =
        [
            nameof(JobUserMapping.User),
            nameof(JobUserMapping.Job)
        ];


        await jobUserMappingRepository.FilterAsync(filter, conditions, x => x.MapToClientJobUserMappingViewModel(), orders, includes: includes);
        return filter;
    }

    #endregion

    #region GetByIdAsync

    public async Task<Result<ClientJobUserMappingDetailsViewModel>> GetByIdAsync(int id, int userId)
    {
        if (id < 1 || userId < 1)
            return Result.Failure<ClientJobUserMappingDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(JobUserMapping.User),
            nameof(JobUserMapping.Job)
        ];

        var result = await jobUserMappingRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted && x.UserId == userId, includes: includes);

        if (result is null)
            return Result.Failure<ClientJobUserMappingDetailsViewModel>(ErrorMessages.NotFoundError);

        return result.MapToClientJobUserMappingDetailsViewModel();
    }

    #endregion

    #endregion
}