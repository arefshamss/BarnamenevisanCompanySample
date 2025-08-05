using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Order;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Order;

public class ClientOrderViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }


    public int UserId { get; set; }


    [Display(Name = "عنوان")] public string Title { get; set; }


    [Display(Name = "تاریخ سفارش")] public DateTime OrderDate { get; set; }


    [Display(Name = "وضعیت سفارش")] public OrderStatus OrderStatus { get; set; }


    [Display(Name = "شماره سفارش")] public string OrderNumber { get; set; }


    [Display(Name = "نوع سفارش")] public List<ClientOrderTypeDetailsViewModel> OrderTypes { get; set; }
}