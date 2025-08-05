using BarnamenevisanCompany.Domain.Models.Project;

namespace BarnamenevisanCompany.Application.Mappers.ProjectMemberMappingMappings;

public static class ProjectMemberMappingMapper
{
    public static ProjectMemberMapping MapTpProjectMemberMapping(short projectId, short memberId,string userPosition) =>
        new()
        {
            ProjectId = projectId,
            UserId = memberId,
            UserPosition = userPosition
        };
}