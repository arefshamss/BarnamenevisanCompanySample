using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

public class ClientCreateJobUserMappingViewModel
{
    public int JobId { get; set; }
    public int UserId { get; set; }    

    
    [Display(Name = "فایل رزومه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public IFormFile Attachment { get; set; }

    
    [Display(Name = "توضیحات")]
    [MaxLength(1200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }
}