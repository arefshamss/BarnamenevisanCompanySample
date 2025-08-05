using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class ProjectGalleryRepository(BarnamenevisanContext context):EfRepository<ProjectGallery,short>(context),IProjectGalleryRepository
{
    
}