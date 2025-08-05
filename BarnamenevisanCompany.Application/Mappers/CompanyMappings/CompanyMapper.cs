using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Models.Company;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Company;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Application.Mappers.CompanyMappings;

public static class CompanyMapper
{
    #region Admin

    public static AdminCompanyViewModel MapToAdminCompanyViewModel(this Company model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            SiteUrl = model.SiteUrl,
            IsDeleted = model.IsDeleted
        };

    public static Company MapToCompany(this AdminCreateCompanyViewModel model) =>
        new()
        {
            Title = model.Title,
            SiteUrl = model.SiteUrl,
            Description = model.Description
        };

    public static AdminUpdateCompanyViewModel MapToAdminUpdateCompanyViewModel(this Company model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            SiteUrl = model.SiteUrl,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
        };

    public static void UpdateCompany(this Company company, AdminUpdateCompanyViewModel model)
    {
        company.Title = model.Title;
        company.SiteUrl = model.SiteUrl;
        company.Description = model.Description;
        company.ImageUrl = model.ImageUrl;
    }

    public static SelectViewModel<short> MapToSelectViewModel(this AdminCompanyViewModel model) =>
        new()
        {
            Id = model.Id,
            DisplayValue = model.Title
        };

    #endregion
}