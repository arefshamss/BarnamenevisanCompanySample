using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class AboutUsCommentRepository(BarnamenevisanContext context):EfRepository<AboutUsComment,short>(context), IAboutUsCommentRepository
{
    
}