using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.JobMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Job;
using BarnamenevisanCompany.Domain.ViewModels.Client.Job;
using BarnamenevisanCompany.Domain.ViewModels.Client.Order;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class JobService(
    IJobRepository jobRepository
)
    : IJobService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterJobViewModel>> FilterAsync(AdminFilterJobViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Job>();
        var orders = Filter.GenerateOrders<Job>();

        var currentDate = DateTime.Now;

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title.Trim()}%"));


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

        switch (filter.JobExpirationStatus)
        {
            case JobExpirationStatus.Active:
                conditions.Add(x => x.ExpireDate >= currentDate);
                break;
            case JobExpirationStatus.All:
                break;
            case JobExpirationStatus.Expired:
                conditions.Add(x => x.ExpireDate < currentDate);
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

        #endregion

        await jobRepository.FilterAsync(filter, conditions, x => x.MapToAdminJobViewModel(), orders);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateJobViewModel model)
    {
        #region Validations

        if (await jobRepository.AnyAsync(x => x.Slug == model.Slug))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "آدرس صفحه"));

        if (model.ExpireDate.ToMiladiDateTime() < DateTime.Now)
            return Result.Failure(string.Format(ErrorMessages.NotValid, "تاریخ"));

        #endregion
        
        var job = model.MapToJob();

        await jobRepository.InsertAsync(job);
        await jobRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateJobViewModel>> FillModelForUpdateAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateJobViewModel>(ErrorMessages.BadRequestError);

        var job = await jobRepository.GetByIdAsync(id);

        if (job is null)
            return Result.Failure<AdminUpdateJobViewModel>(ErrorMessages.NotFoundError);

        return job.MapToAdminUpdateJobViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateJobViewModel model)
    {
        #region Validations

        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (await jobRepository.AnyAsync(x => x.Slug == model.Slug && x.Id != model.Id))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "آدرس صفحه"));
        
        if (model.ExpireDate.ToMiladiDateTime() < DateTime.Now)
            return Result.Failure(string.Format(ErrorMessages.NotValid, "تاریخ"));

        #endregion

        var job = await jobRepository.GetByIdAsync(model.Id);

        if (job is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        job.MapToJob(model);
        jobRepository.Update(job);
        await jobRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(int id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var job = await jobRepository.GetByIdAsync(id);

        var result = jobRepository.SoftDeleteOrRecover(job);
        await jobRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #endregion

    #region Client

    #region FilterAsync

    public async Task<Result<ClientFilterJobViewModel>> FilterAsync(ClientFilterJobViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Job>();
        var orders = Filter.GenerateOrders<Job>();

        conditions.Add(x => !x.IsDeleted);

        conditions.Add(x => x.ExpireDate >= DateTime.Now);

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);

        await jobRepository.FilterAsync(filter, conditions, x => x.MapToClientJobViewModel(), orders);
        return filter;
    }

    #endregion

    #region GetSlugByIdAsync

    public async Task<Result<string>> GetSlugByIdAsync(int id)
    {
        if (id < 1)
            return Result.Failure<string>(ErrorMessages.BadRequestError);

        var result = await jobRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (result is null)
            return Result.Failure<string>(ErrorMessages.NotFoundError);

        return result.Slug;
    }

    #endregion
    
    #region GetBySlugAsync

    public async Task<Result<ClientJobDetailsViewModel>> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            return Result.Failure<ClientJobDetailsViewModel>(ErrorMessages.BadRequestError);

        var blog = await jobRepository.FirstOrDefaultAsync(x => x.Slug == slug && !x.IsDeleted);

        if (blog is null)
            return Result.Failure<ClientJobDetailsViewModel>(ErrorMessages.NotFoundError);

        return blog.MapToClientJobDetailsViewModel();
    }

    #endregion

    #region IsActiveJobExistsAsync

    public async Task<bool> IsActiveJobExistsAsync()  
    {
        return await jobRepository.AnyAsync(x => x.ExpireDate >= DateTime.Now.Date && !x.IsDeleted);
    }

    #endregion
    
    #endregion
}