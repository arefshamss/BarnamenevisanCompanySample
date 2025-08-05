using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.User;
using BarnamenevisanCompany.Domain.ViewModels.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;

public class AdminFilterUserPositionViewModel:BasePaging<AdminUserPositionViewModel>
{
   
    
    [FilterInput]
    [Display(Name = "سمت کاربر")]
    public string? Position { get; set; }
    [FilterInput]
    [Display(Name = "مرتب سازی بر اساس")]
    public UserPositionStatus UserPositionStatus { get; set; }
}