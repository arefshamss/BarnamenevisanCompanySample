using BarnamenevisanCompany.Application.Mappers.HonorsGalleryMappings;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Admin.HonorsGallery;
using BarnamenevisanCompany.Domain.ViewModels.Client.Honors;
using BarnamenevisanCompany.Domain.ViewModels.Client.HonorsGallery;

namespace BarnamenevisanCompany.Application.Mappers.HonorsMappings;

public static class HonorsMapper
{
    public static Honors MapToHonors(this AdminCreateHonorsViewModel honors) =>
        new()
        {
            Title = honors.Title,
            Description = honors.Description,
            CompanyId = honors.CompanyId,
            TeaserName = honors.TeaserName,
            TeaserPoster = honors.TeaserPosterName,
            ImageName = honors.ImageName,
            Slug = honors.Slug
        };


    public static AdminUpdateHonorsViewModel MapToAdminCreateHonorsViewModel(this Honors honors) =>
        new()
        {
            Title = honors.Title,
            Description = honors.Description,
            ImageName = honors.ImageName,
            TeaserName = honors.TeaserName,
            TeaserPosterImageName = honors.TeaserPoster,
            HonorsId = honors.Id,
            CompanyId = honors.CompanyId,
            Slug = honors.Slug
        };


    public static AdminHonorsGalleryViewModel MapToHonorsGallery(this HonorsGallery honors) =>
        new()
        {
            ImageName = honors.ImageName,
            HonorId = honors.Id,
        };

    public static void MapToHonors(this Honors honors, AdminUpdateHonorsViewModel honorsViewModel)
    {
        honors.Title = honorsViewModel.Title;
        honors.Description = honorsViewModel.Description;
        honors.ImageName = honorsViewModel.ImageName;
        honors.TeaserName = honorsViewModel.TeaserName;
        honors.TeaserPoster = honorsViewModel.TeaserPosterImageName;
        honors.CompanyId = honorsViewModel.CompanyId;
        honors.Slug = honorsViewModel.Slug;
    }

    public static AdminHonorsViewModel MapToAdminHonorsViewModel(this Honors honors) =>
        new()
        {
            Title = honors.Title,
            ImageName = honors.ImageName,
            IsDeleted = honors.IsDeleted,
            CreateDate = honors.CreatedDate,
            HonorId = honors.Id,
        };


    public static AdminHonorsGalleryViewModel MapToAdminHonorsViewModel(this HonorsGallery honors) =>
        new()
        {
            ImageName = honors.ImageName,
            HonorId = honors.Id,
            Path = FilePaths.HonorsGalleryImage + honors.ImageName
        };

    public static ClientHonorsViewModel MapToClientHonorsViewModel(this Honors honors) =>
        new()
        {
            ImageName = honors.ImageName,
            Title = honors.Title,
            slug = honors.Slug,
        };


    public static ClientHonorDetailViewModel MapToClientHonorDetailViewModel(this Honors honors) =>
        new()
        {
            ImageName = honors.ImageName,
            Title = honors.Title,
            CompanyName = honors.Company.Title,
            Description = honors.Description,
            TeaserName = honors.TeaserName,
            TeaserPoster = honors.TeaserPoster,
            HonorGallery = honors.HonorsGalleries.Select(s => s.MapToClientHonorGalleryViewModel()).ToList()
        };
}