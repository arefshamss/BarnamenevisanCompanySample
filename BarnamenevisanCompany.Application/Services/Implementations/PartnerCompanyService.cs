using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.CompanyMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Company;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;
using BarnamenevisanCompany.Domain.ViewModels.Client.PartnerCompany;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class PartnerCompanyService(
    IPartnerCompanyRepository partnerCompanyRepository
) : IPartnerCompanyService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterPartnerCompanyViewModel>> FilterAsync(AdminFilterPartnerCompanyViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<PartnerCompany>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title}%"));

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

        await partnerCompanyRepository.FilterAsync(filter, conditions, x => x.MapToAdminPartnerCompanyViewModel());
        return filter;
    }
    
    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreatePartnerCompanyViewModel model)
    {
        var partnerCompany = model.MapToPartnerCompany();


        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.PartnerCompanyOriginalImage);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            partnerCompany.ImageUrl = result.Value;
        }
        else
        {
            partnerCompany.ImageUrl = SiteTools.DefaultImageName;
        }

        await partnerCompanyRepository.InsertAsync(partnerCompany);
        await partnerCompanyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdatePartnerCompanyViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdatePartnerCompanyViewModel>(ErrorMessages.BadRequestError);

        var partnerCompany = await partnerCompanyRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (partnerCompany is null)
            return Result.Failure<AdminUpdatePartnerCompanyViewModel>(ErrorMessages.NotFoundError);

        return partnerCompany.MapToAdminUpdatePartnerCompanyViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdatePartnerCompanyViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var partnerCompany = await partnerCompanyRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (partnerCompany is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        partnerCompany.UpdatePartnerCompany(model);

        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.PartnerCompanyOriginalImage, deleteFileName: partnerCompany.ImageUrl);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            partnerCompany.ImageUrl = result.Value;
        }

        partnerCompanyRepository.Update(partnerCompany);
        await partnerCompanyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var company = await partnerCompanyRepository.GetByIdAsync(id);

        if (company is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = partnerCompanyRepository.SoftDeleteOrRecover(company);
        await partnerCompanyRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

#endregion

    #endregion

    #region Client

    #region FilterAsync

    public async Task<Result<ClientFilterPartnerCompanyViewModel>> FilterAsync(ClientFilterPartnerCompanyViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<PartnerCompany>();
        var orders = Filter.GenerateOrders<PartnerCompany>();

        conditions.Add(x => !x.IsDeleted);

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);

        await partnerCompanyRepository.FilterAsync(filter, conditions, x => x.MapToClientPartnerCompanyViewModel(), orders);
        return filter;
    }

#endregion

    #endregion
}