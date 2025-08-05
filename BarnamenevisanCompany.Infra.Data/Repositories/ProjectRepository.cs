using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class ProjectRepository(BarnamenevisanContext context):EfRepository<Project,short>(context),IProjectRepository
{
    
}