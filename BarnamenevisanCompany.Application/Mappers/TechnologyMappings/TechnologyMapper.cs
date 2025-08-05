using BarnamenevisanCompany.Domain.Models.Technology;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;

namespace BarnamenevisanCompany.Application.Mappers.TechnologyMappings;

public static class TechnologyMapper
{
    public static AdminTechnologyViewModel MapToAdminTechnologyViewModel(this Technology technology) =>
        new()
        {
            Id = technology.Id,
            Title = technology.Title,
            CreateDate = technology.CreatedDate,
            ImageName = technology.IconName,
            IsDeleted = technology.IsDeleted
        };

    public static Technology MapToTechnology(this AdminCreateTechnologyViewModel model) =>
        new()
        {
            Title = model.Title,
            IconName = model.ImageName,
        };

    public static AdminUpdateTechnologyViewModel MapToAdminUpdateTechnologyViewModel(this Technology model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ImageName = model.IconName,
        };

    public static void MaptoTechnology(this Technology model, AdminUpdateTechnologyViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.IconName = viewModel.ImageName;
    }
}