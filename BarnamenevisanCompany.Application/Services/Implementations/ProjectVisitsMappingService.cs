using BarnamenevisanCompany.Application.Mappers.ProjecVisitsMappingMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ProjectVisitsMappingService(IProjectVisitsMappingRepository projectVisitsMappingRepository) : IProjectVisitsMappingService
{
    public async Task<Result> RegisterProjectVisitAsync(short projectId, string userIp, int? userId)
    {
        if (projectId == null || userIp == null)
            return Result.Failure(ErrorMessages.BadRequestError);

        var visitExist = await projectVisitsMappingRepository
            .AnyAsync(x => x.ProjectId == projectId && x.UserId == userId);
        if (visitExist)
            return Result.Success("ok");

        var newVisit = ProjectVisitsMappingMapper.MapToProjectVisitsMapping(projectId, userId, userIp);
        
        await projectVisitsMappingRepository.InsertAsync(newVisit);
        await projectVisitsMappingRepository.SaveChangesAsync();
        
        return Result.Success(SuccessMessages.InsertConsultSuccessfully);
    }
}