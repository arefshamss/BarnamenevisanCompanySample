using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class ProjectMemberMappingRepository(BarnamenevisanContext context):EfRepository<ProjectMemberMapping,short>(context),IProjectMemberMappingRepository
{
    
}