using BarnamenevisanCompany.Application.Mappers.ProjectGalleryMappings;
using BarnamenevisanCompany.Application.Mappers.TechnologyMappings;
using BarnamenevisanCompany.Application.Mappers.UserPositionMappings;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Project;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;

namespace BarnamenevisanCompany.Application.Mappers.ProjectMappings;

public static class ProjectMapper
{
    public static AdminProjectViewModel MapToAdminProjectViewModel(this Project project) =>
        new()
        {
            Id = project.Id,
            Title = project.Title,
            Url = project.SiteUrl,
            ImageName = project.ImageName,
            IsDeleted = project.IsDeleted,
            CreateDate = project.CreatedDate,
            Priority = project.Priority,
        };

    public static Project MapToProject(this AdminCreateProjectViewModel model) =>
        new()
        {
            Title = model.Title,
            SiteUrl = model.SiteUrl,
            ImageName = model.ImageName,
            TeaserName = model.TeaserName,
            TeaserPoster = model.TeaserPosterName,
            Description = model.Description,
            ProgressRate = model.ProgressByte,
            Priority = model.Priority,
            Slug = model.Slug,
        };

    public static AdminUpdateProjectViewModel MapToAdminUpdateProjectViewModel(this Project project, string? programmerId, string? technologyId, string? programmerPosition) =>
        new()
        {
            Id = project.Id,
            Title = project.Title,
            SiteUrl = project.SiteUrl,
            ImageName = project.ImageName,
            TeaserPosterName = project.TeaserPoster,
            Description = project.Description,
            Priority = project.Priority,
            ProgressByte = project.ProgressRate,
            Programmer = programmerId,
            Slug = project.Slug,
            Technology = technologyId,
            TeaserName = project.TeaserName,
            ProgrammerPositions = programmerPosition
        };

    public static void MapToProject(this Project model, AdminUpdateProjectViewModel viewModel)
    {
        model.Title = viewModel.Title;
        model.SiteUrl = viewModel.SiteUrl;
        model.ImageName = viewModel.ImageName;
        model.Description = viewModel.Description;
        model.ProgressRate = viewModel.ProgressByte;
        model.TeaserName = viewModel.TeaserName;
        model.TeaserPoster = viewModel.TeaserPosterName;
        model.Slug = viewModel.Slug;
        model.Priority = viewModel.Priority;
    }

    public static ClientProjectDetailViewModel MapToClientProjectDetailViewModel(this Project project) =>
        new()
        {
            Id = project.Id,
            Title = project.Title,
            SiteUrl = project.SiteUrl,
            ImageName = project.ImageName,
            TeaserName = project.TeaserName,
            ProgressByte = project.ProgressRate,
            TeaserPoster = project.TeaserPoster,
            Descripition = project.Description,
            Views = project.ProjectVisitsMappings.Count,
            TechnologyGallery = project.ProjectGalleries.Where(s => !s.IsDeleted).Select(x => x.MapToAdminProjectGalleryViewModel()).ToList(),
            Technologys = project.ProjectTechnologyMappings.Where(s => !s.IsDeleted).Select(x => x.Technology.MapToAdminTechnologyViewModel()).ToList(),
            Programmers = project.ProjectMemberMappings.Where(s => !s.IsDeleted && !s.User.IsDeleted).Select(x => x.User.MapToClientProjectMemberViewModel(project.Id)).OrderBy(s => s.Priority).ToList()
        };

    public static ClientProjectViewModel MapToClientProjectViewModel(this Project project) =>
        new()
        {
            Id = project.Id,
            ImageName = project.ImageName,
            Description = project.Description,
            Priority = project.Priority,
            ProgressRate = project.ProgressRate,
            Title = project.Title,
            Slug = project.Slug
        };
}