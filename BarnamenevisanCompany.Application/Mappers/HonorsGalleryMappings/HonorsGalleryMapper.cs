using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using BarnamenevisanCompany.Domain.ViewModels.Client.HonorsGallery;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Application.Mappers.HonorsGalleryMappings;

public static class HonorsGalleryMapper
{
    public static ClientHonorGalleryViewModel MapToClientHonorGalleryViewModel(this HonorsGallery honorsGallery) =>
        new()
        {
            ImageName = honorsGallery.ImageName,
            HonorName = honorsGallery.Honors.Title
        };

    public static HonorsGallery MapToHonorsGallery(short id, string imageName) =>
        new()
        {
            ImageName = imageName,
            HonorsId = id
        };

    public static AdminHonorsGalleryViewModel MapToAdminHonorsGalleryViewModel(short id, string imageName) =>
        new()
        {
            ImageName = imageName,
            HonorId = id,
            Path = FilePaths.HonorsGalleryImage
        };
}