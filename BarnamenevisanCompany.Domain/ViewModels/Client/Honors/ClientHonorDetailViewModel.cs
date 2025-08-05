using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.ViewModels.Client.HonorsGallery;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Honors;

public class ClientHonorDetailViewModel
{
    [Display(Name = "عنوان")]
    public string Title { get; set; }

    public string ImageName { get; set; }

    public string? TeaserName { get; set; }
    [Display(Name = "توضیحات")]
    public string Description { get; set; }
    [Display(Name = "شرکت")]
    public string CompanyName { get; set; } 

    public string? TeaserPoster { get; set; }
    
    
    public List<ClientHonorGalleryViewModel>? HonorGallery { get; set; }
}