using BarnamenevisanCompany.Domain.Models.Project;

namespace BarnamenevisanCompany.Application.Mappers.ProjectTechnologyMappingMappings;

public static class ProjectTechnologyMappingMapper
{
    public static ProjectTechnologyMapping MapToProjectTechnologyMapping(short technologyId,short projectId) =>
        new()
        {
            TechnologyId = technologyId,
            ProjectId = projectId
        };
}