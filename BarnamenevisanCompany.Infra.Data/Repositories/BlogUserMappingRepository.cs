using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Blog;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class BlogUserMappingRepository(BarnamenevisanContext context) : EfRepository<BlogUserMapping>(context), IBlogUserMappingRepository
{
    
}