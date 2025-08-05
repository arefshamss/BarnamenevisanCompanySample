using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;

public class AdminCreateAboutUsCommentViewModel
{
    public int UserId { get; set; }

    [Display(Name = "نمره کامنت")]
    [Range(1, 5, ErrorMessage = ErrorMessages.RangeError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    
    public byte Rating { get; set; }

    [Display(Name = "متن کامنت")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Comment { get; set; }
}