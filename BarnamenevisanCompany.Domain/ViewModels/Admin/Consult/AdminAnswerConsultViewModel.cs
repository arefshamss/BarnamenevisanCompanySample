using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;

public class AdminAnswerConsultViewModel
{
    public short ConsultId { get; set; }

    public string UserMobile { get; set; }

    [Display(Name = "پاسخ ")]
    [MaxLength(500, ErrorMessage = ErrorMessages.MaxLengthError)]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string AdminMessage { get; set; }
}