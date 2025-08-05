using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;

public class AdminUpdateTechnologyViewModel
{
    public short Id { get; set; }
    [Display(Name = "عنوان")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string Title { get; set; }

    public string ImageName { get; set; }

    [Display(Name = "تصویر")]

    public IFormFile? Image { get; set; }
}