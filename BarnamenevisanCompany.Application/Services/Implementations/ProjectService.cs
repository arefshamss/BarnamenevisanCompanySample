using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.ProjectMappings;
using BarnamenevisanCompany.Application.Mappers.ProjectMemberMappingMappings;
using BarnamenevisanCompany.Application.Mappers.ProjectTechnologyMappingMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Project;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Project;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;
using BarnamenevisanCompany.Domain.ViewModels.Client.Project;
using BarnamenevisanCompany.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ProjectService(
    IProjectRepository projectRepository,
    ITechnologyRepository technologyRepository,
    IProjectTechnologyMappingRepository projectTechnologyMappingRepository,
    IProjectMemberMappingRepository projectMemberMappingRepository,
    IProjectGalleryRepository projectGalleryRepository,
    IProjectVisitsMappingRepository projectVisitsMappingRepository,
    IUserPositionRepository userPositionRepository) : IProjectService
{
    #region FilterAsync

    public async Task<AdminFilterProjectViewModel> FilterAsync(AdminFilterProjectViewModel filter)
    {
        filter = filter ?? new AdminFilterProjectViewModel();
        var condition = Filter.GenerateConditions<Project>();
        var order = Filter.GenerateOrders<Project>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));
        switch (filter.DeleteStatus)
        {
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                condition.Add(s => s.IsDeleted);
                break;
            case DeleteStatus.NotDeleted:
                condition.Add(s => !s.IsDeleted);
                break;
        }

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Ascending:
                order.Add(s => s.CreatedDate);
                break;
            case FilterOrderBy.Descending:
                break;
        }

        switch (filter.ProjectStatus)
        {
            case ProjectStatus.None:
                break;
            case ProjectStatus.Priority:
                order.Add(s => s.Priority);
                break;
        }

        #endregion

        await projectRepository.FilterAsync(filter, condition, s => s.MapToAdminProjectViewModel(), order);

        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateProjectViewModel model)
    {
        var imageName = await model.Image.AddImageToServer(FilePaths.ProjectImage, 340, 200, FilePaths.ProjectImageThumb);

        if (await projectRepository.AnyAsync(s => s.Priority == model.Priority && !s.IsDeleted))
            return Result.Failure(ErrorMessages.ProjectPriorityExist);

        if (model.TeaserPoster != null)
        {
            var result = await model.TeaserPoster.AddImageToServer(FilePaths.ProjectTeaserPoster);
            if (result.IsFailure) return Result.Failure(ErrorMessages.SomethingWentWrong);
            model.TeaserPosterName = result.Value;
        }

        model.ImageName = imageName.Value;

        var newProject = model.MapToProject();

        await projectRepository.InsertAsync(newProject);
        await projectRepository.SaveChangesAsync();
        if (model.Programmer is not null && model.ProgrammerPositions is not null)
        {
            var programmerIds = model.Programmer.Split(',').Select(short.Parse).ToArray();
            var programmerPositions = model.ProgrammerPositions.Split(',');


            for (int i = 0; i < programmerIds.Length; i++)
            {
                var porogrammerid = programmerIds[i];
                var porogrammerPosition = programmerPositions[i];
                var memberid = await userPositionRepository.FirstOrDefaultAsync(s => s.UserId == porogrammerid);
                await projectMemberMappingRepository.InsertAsync(ProjectMemberMappingMapper.MapTpProjectMemberMapping(newProject.Id, memberid.Id, porogrammerPosition));
            }
        }

        if (model.Technology is not null)
        {
            var technologyIds = model.Technology.ConvertStringToShortList();
            foreach (var id in technologyIds)
            {
                await projectTechnologyMappingRepository.InsertAsync(ProjectTechnologyMappingMapper.MapToProjectTechnologyMapping(id, newProject.Id));
            }
        }

        await projectRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdate

    public async Task<Result<AdminUpdateProjectViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateProjectViewModel>(ErrorMessages.BadRequestError);

        var programmerId = await projectMemberMappingRepository
            .GetAllAsync(u => u.User.UserId, u => u.ProjectId == id && !u.IsDeleted,includes: nameof(ProjectMemberMapping.User));

        var programmerPosition = await projectMemberMappingRepository
            .GetAllAsync(u => u.UserPosition, u => u.ProjectId == id && !u.IsDeleted);

        var programmer = programmerId.ConvertIntListToString();

        var programmerp = string.Join(',', programmerPosition);

        var technologyId = await projectTechnologyMappingRepository
            .GetAllAsync(u => u.TechnologyId, u => u.ProjectId == id && !u.IsDeleted);

        var technology = technologyId.ConvertShortListToString();

        var project = await projectRepository.GetByIdAsync(id);

        if (project is null)
            return Result.Failure<AdminUpdateProjectViewModel>(ErrorMessages.NotFoundError);

        return project.MapToAdminUpdateProjectViewModel(programmer, technology, programmerp);
    }

    #endregion

    #region Update

    public async Task<Result> UpdateAsync(AdminUpdateProjectViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var project = await projectRepository.GetByIdAsync(model.Id);

        if (project is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (project.Priority != model.Priority)
        {
            if (await projectRepository.AnyAsync(s => s.Priority == model.Priority && !s.IsDeleted))
                return Result.Failure(ErrorMessages.ProjectPriorityExist);
        }

        if (model.TeaserName != null && model.TeaserName != project.TeaserName)
        {
            project.TeaserName.DeleteFile(FilePaths.ProjectTeaser);
        }

        if (model.TeaserPoster != null)
        {
            if (model.TeaserPosterName != null) model.TeaserPosterName.DeleteImage(FilePaths.ProjectTeaserPoster);
            var result = await model.TeaserPoster.AddImageToServer(FilePaths.ProjectTeaserPoster);
            if (result.IsFailure) return Result.Failure(ErrorMessages.SomethingWentWrong);
            model.TeaserPosterName = result.Value;
        }

        if (model.Image is not null)
        {
            var imageName = await model.Image
                .AddImageToServer(FilePaths.ProjectImage, 340, 200, FilePaths.ProjectImageThumb, deleteFileName: model.ImageName);
            model.ImageName = imageName.Value;
        }

        if (model.Programmer is null)
        {
            if (await projectMemberMappingRepository.AnyAsync(s => s.ProjectId == model.Id))
            {
                await projectMemberMappingRepository.ExecuteDeleteRange(u => u.ProjectId == model.Id);
            }
        }

        if (model.Programmer is not null && model.ProgrammerPositions is not null)
        {
            await projectMemberMappingRepository.ExecuteDeleteRange(u => u.ProjectId == model.Id);

            var programmerIds = model.Programmer.Split(',').Select(short.Parse).ToArray();
            var programmerPositions = model.ProgrammerPositions.Split(',');

            for (int i = 0; i < programmerIds.Length; i++)
            {
                var porogrammerid = programmerIds[i];
                var porogrammerPosition = programmerPositions[i];
                var memberid = await userPositionRepository.FirstOrDefaultAsync(s => s.UserId == porogrammerid);
                await projectMemberMappingRepository.InsertAsync(ProjectMemberMappingMapper.MapTpProjectMemberMapping(project.Id, memberid.Id, porogrammerPosition));
            }
        }

        if (model.Technology is not null)
        {
            var technologyIds = model.Technology.ConvertStringToShortList();

            await projectTechnologyMappingRepository.ExecuteDeleteRange(u => u.ProjectId == model.Id);

            await projectTechnologyMappingRepository.InsertRangeAsync(technologyIds
                .Select(x => ProjectTechnologyMappingMapper.MapToProjectTechnologyMapping(x, project.Id))
                .ToList());
        }

        project.MapToProject(model);
        projectRepository.Update(project);
        await projectRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var project = await projectRepository.GetByIdAsync(id);

        if (project is null) return Result.Failure(ErrorMessages.NotFoundError);

        if (project.TeaserName != null)
        {
            if (project.TeaserPoster != null) project.TeaserPoster.DeleteFile(FilePaths.ProjectTeaser);
            project.TeaserName.DeleteFile(FilePaths.ProjectTeaser);
            project.TeaserName = null;
        }


        var galleryImageName = await projectGalleryRepository.GetAllAsync(x => x.ImageName, x => x.ProjectId == id);

        foreach (var imageName in galleryImageName)
        {
            imageName.DeleteImage(FilePaths.ProjectGallery);
        }

        await projectGalleryRepository.ExecuteDeleteRange(x => x.ProjectId == id);


        await projectMemberMappingRepository.ExecuteDeleteRange(x => x.ProjectId == id);

        await projectTechnologyMappingRepository.ExecuteDeleteRange(x => x.ProjectId == id);

        var result = projectRepository.SoftDeleteOrRecover(project);
        await projectRepository.SaveChangesAsync();
        if (result)
            return Result.Success(SuccessMessages.DeleteSuccess);

        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region FillModelForDetailAsync

    public async Task<Result<ClientProjectDetailViewModel>> FillModelDetailForClient(short id)
    {
        if (id < 1)
            return Result.Failure<ClientProjectDetailViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(Project.ProjectMemberMappings),
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}",
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}.{nameof(UserPosition.Users)}",
            nameof(Project.ProjectTechnologyMappings),
            $"{nameof(Project.ProjectTechnologyMappings)}.{nameof(ProjectTechnologyMapping.Technology)}",

            nameof(Project.ProjectGalleries),
            nameof(Project.ProjectVisitsMappings),

            nameof(Project.ProjectMemberMappings),
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}",
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}.{nameof(UserPosition.Users)}",
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}.{nameof(UserPosition.Users)}.{nameof(User.UserSocialNetworkMappings)}",
            $"{nameof(Project.ProjectMemberMappings)}.{nameof(ProjectMemberMapping.User)}.{nameof(UserPosition.Users)}.{nameof(User.UserSocialNetworkMappings)}.{nameof(UserSocialNetworkMapping.UserSocialNetwork)}",
        ];

        var project = await projectRepository.FirstOrDefaultAsync(x => x.Id == id, includes: includes);

        if (project is null)
            return Result.Failure<ClientProjectDetailViewModel>(ErrorMessages.NotFoundError);

        var projectDetail = project.MapToClientProjectDetailViewModel();
        return projectDetail;
    }

    #endregion

    #region GetAllAsync

    public async Task<ClientFilterProjectViewModel> GetAllAsync(ClientFilterProjectViewModel model)
    {
        var condition = Filter.GenerateConditions<Project>();
        var order = Filter.GenerateOrders<Project>();
        order.Add(s => s.Priority, FilterOrderBy.Ascending);
        order.Add(s => s.CreatedDate, FilterOrderBy.Ascending);


        condition.Add(s => !s.IsDeleted);

        await projectRepository.FilterAsync(model, condition, s => s.MapToClientProjectViewModel(), orderConditions: order);

        return model;
    }

    #endregion

    #region DeleteTeaser

    public async Task<Result> DeleteTeaserAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);
        var project = await projectRepository.GetByIdAsync(id);

        if (project is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        project.TeaserName.DeleteFile(FilePaths.ProjectTeaser);

        project.TeaserName = null;
        projectRepository.Update(project);
        await projectRepository.SaveChangesAsync();


        return Result.Success(SuccessMessages.DeleteSuccess);
    }

    #endregion
}