using BarnamenevisanCompany.Domain.Models.DynamicPage;
using BarnamenevisanCompany.Domain.ViewModels.Admin.DynamicPage;
using BarnamenevisanCompany.Domain.ViewModels.Client.DynamicPage;

namespace BarnamenevisanCompany.Application.Mappers.DynamicPageMappings;

public static class DynamicPageMapper
{
    #region Admin

    public static AdminDynamicPageViewModel MapToAdminDynamicPageViewModel(this DynamicPage model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            IsActive = model.IsActive,
            IsDeleted = model.IsDeleted,
            CreatedDate = model.CreatedDate,
        };

    public static DynamicPage MapToDynamicPage(this AdminCreateDynamicPageViewModel model) =>
        new()
        {
            Title = model.Title,
            Slug = model.Slug,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            IsActive = model.IsActive
        };

    public static AdminUpdateDynamicPageViewModel MapToAdminUpdateDynamicPageViewModel(this DynamicPage model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Slug = model.Slug,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            IsActive = model.IsActive
        };

    public static void UpdateDynamicPage(this DynamicPage model, AdminUpdateDynamicPageViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.Slug = viewModel.Slug;
        model.ShortDescription = viewModel.ShortDescription;
        model.Description = viewModel.Description;
        model.IsActive = viewModel.IsActive;
    }

    #endregion

    #region Client

    public static ClientDynamicPageViewModel MapToClientDynamicPageViewModel(this DynamicPage model) =>
        new()
        {
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
        };

    #endregion
}