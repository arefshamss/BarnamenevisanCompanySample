using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ProjectGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Mappers.ProjectGalleryMappings;

public static class ProjectGalleryMapper
{
    public static AdminProjectGalleryViewModel MapToAdminProjectGalleryViewModel(this ProjectGallery projectGallery) =>
        new()
        {
            Path = FilePaths.ProjectGallery,
            ProjectId = projectGallery.ProjectId,
            ImageName = projectGallery.ImageName,
        };

    public static AdminProjectGalleryViewModel MapToAdminProjectGalleryViewModel(short id, string imageName) =>
        new()
        {
            Path = FilePaths.ProjectGallery,
            ProjectId = id,
            ImageName = imageName,
        };

    public static ProjectGallery MapToProjectGallery(short id, string imageName) =>
        new()
        {
            ProjectId = id,
            ImageName = imageName,
        };
}