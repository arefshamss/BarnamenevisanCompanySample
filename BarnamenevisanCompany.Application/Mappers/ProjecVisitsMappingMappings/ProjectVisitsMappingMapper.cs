using BarnamenevisanCompany.Domain.Models.Project;

namespace BarnamenevisanCompany.Application.Mappers.ProjecVisitsMappingMappings;

public static class ProjectVisitsMappingMapper
{
    public static ProjectVisitsMapping MapToProjectVisitsMapping(short projectId, int? userId, string userIp) =>
        new()
        {
            ProjectId = projectId,
            UserId = userId,
            UserIp = userIp
        };
}