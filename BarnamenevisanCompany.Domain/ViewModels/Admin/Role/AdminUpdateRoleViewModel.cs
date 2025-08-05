using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Role;

public class AdminUpdateRoleViewModel
{
    public short Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string Name { get; set; }
}