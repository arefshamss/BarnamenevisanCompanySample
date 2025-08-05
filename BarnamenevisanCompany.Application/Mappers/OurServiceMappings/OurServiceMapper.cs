using BarnamenevisanCompany.Domain.ViewModels.Admin.OurService;
using BarnamenevisanCompany.Domain.ViewModels.Client.OurService;

namespace BarnamenevisanCompany.Application.Mappers.OurServiceMappings;

public static class OurServiceMapper
{
    public static Domain.Models.OurServices.OurService MapTOurService(this AdminCreateOurServiceViewModel ourService) =>
        new()
        {
            ImageName = ourService.ImageName,
            Title = ourService.Title,
            ShortDescription = ourService.ShortDescription,
            LongDescription = ourService.LongDescription,
            Slug = ourService.Slug.Trim(),
        };

    public static void MapTOurService(this Domain.Models.OurServices.OurService ourService, AdminUpdateOurServiceViewModel model)
    {
        ourService.ImageName = model.ImageName;
        ourService.Title = model.Title;
        ourService.LongDescription = model.LongDescription;
        ourService.ShortDescription = model.ShortDescription;
        ourService.Slug = model.Slug.Trim();
    }

    public static AdminOurServiceViewModel MapTOurService(this Domain.Models.OurServices.OurService model) =>
        new()
        {
            Title = model.Title,
            ImageName = model.ImageName,
            Id = model.Id,
            IsDeleted = model.IsDeleted,
            CreateDate = model.CreatedDate
        };

    public static AdminUpdateOurServiceViewModel MapToAdminUpdateOurServiceViewModel(this Domain.Models.OurServices.OurService model) =>
        new()
        {
            Id = model.Id,
            ImageName = model.ImageName,
            Title = model.Title,
            LongDescription = model.LongDescription,
            ShortDescription = model.ShortDescription,
            Slug = model.Slug,
        };

    public static ClientGetAllServiceViewModel MapToClientGetAllServiceViewModel(this Domain.Models.OurServices.OurService model) =>
        new()
        {
            ImageName = model.ImageName,
            Title = model.Title,
            Id = model.Id,
            ShortDescription = model.ShortDescription,
            Slug = model.Slug
        };

    public static ClientDetailServiceViewModel MapToClientDetailServiceViewModel(this Domain.Models.OurServices.OurService model) =>
        new()
        {
            ImageName = model.ImageName,
            Title = model.Title,
            Id = model.Id,
            LongDescription = model.LongDescription,
            ShortDescription = model.ShortDescription,
        };
}