using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Order;

namespace BarnamenevisanCompany.Domain.ViewModels.Admin.Order;

public class AdminAnswerToOrderViewModel
{
    public int OrderId { get; set; }
    
    
    [Display(Name="ارسال پاسخ")]
    public string? Answer { get; set; }
}