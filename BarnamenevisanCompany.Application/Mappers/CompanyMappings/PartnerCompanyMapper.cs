using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Models.Company;
using BarnamenevisanCompany.Domain.ViewModels.Admin.PartnerCompany;
using BarnamenevisanCompany.Domain.ViewModels.Client.PartnerCompany;

namespace BarnamenevisanCompany.Application.Mappers.CompanyMappings;

public static class PartnerCompanyMapper
{
    #region Admin

    public static AdminPartnerCompanyViewModel MapToAdminPartnerCompanyViewModel(this PartnerCompany model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            SiteUrl = model.SiteUrl,
            IsDeleted = model.IsDeleted,
        };

    public static PartnerCompany MapToPartnerCompany(this AdminCreatePartnerCompanyViewModel model) =>
        new()
        {
            Title = model.Title,
            SiteUrl = model.SiteUrl,
            ShortDescription = model.ShortDescription,
        };

    public static AdminUpdatePartnerCompanyViewModel MapToAdminUpdatePartnerCompanyViewModel(this PartnerCompany model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            SiteUrl = model.SiteUrl,
            ImageUrl = model.ImageUrl,
            ShortDescription = model.ShortDescription,
        };

    public static void UpdatePartnerCompany(this PartnerCompany model, AdminUpdatePartnerCompanyViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.SiteUrl = viewModel.SiteUrl;
        model.ShortDescription = viewModel.ShortDescription;
        model.ImageUrl = viewModel.ImageUrl;
    }

    #endregion

    #region Client

    public static ClientPartnerCompanyViewModel MapToClientPartnerCompanyViewModel(this PartnerCompany model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageUrl = model.ImageUrl,
            SiteUrl = model.SiteUrl,
        };

    #endregion
}