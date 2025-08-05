using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.CompanyMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Company;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Company;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class CompanyService(
    ICompanyRepository companyRepository
) : ICompanyService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterCompanyViewModel>> FilterAsync(AdminFilterCompanyViewModel filter)
    {
        filter??=new();

        var conditions = Filter.GenerateConditions<Company>();

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

        #endregion
        
        await companyRepository.FilterAsync(filter, conditions, x => x.MapToAdminCompanyViewModel());
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateCompanyViewModel model)
    {
        var company = model.MapToCompany();
        
        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.CompanyOriginalImage);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            company.ImageUrl = result.Value;
        }
        else
        {
            company.ImageUrl = SiteTools.DefaultImageName;
        }
        
        await companyRepository.InsertAsync(company);
        await companyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminUpdateCompanyViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateCompanyViewModel>(ErrorMessages.BadRequestError);

        var company = await companyRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (company is null)
            return Result.Failure<AdminUpdateCompanyViewModel>(ErrorMessages.NotFoundError);

        return company.MapToAdminUpdateCompanyViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateCompanyViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var company = await companyRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (company is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        company.UpdateCompany(model);

        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.CompanyOriginalImage, deleteFileName: company.ImageUrl);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            company.ImageUrl = result.Value;
        }

        companyRepository.Update(company);
        await companyRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var company = await companyRepository.GetByIdAsync(id);

        if (company is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = companyRepository.SoftDeleteOrRecover(company);
        await companyRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

 

    #endregion

    #region SelectList

    public async Task<SelectList> AdminGetSelectListAsync()
    {
        var companies = await companyRepository.GetAllAsync(u=> u.MapToAdminCompanyViewModel(),u=> !u.IsDeleted);
        var list = companies.Select(u => u.MapToSelectViewModel()).ToList();
        return list.ToSelectList();
    }

#endregion

    #endregion
}