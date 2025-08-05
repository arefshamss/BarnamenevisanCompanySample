using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Enums.Job;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Job;

public class ClientJobViewModel
{
    [Display(Name = "شناسه")] public int Id { get; set; }
    
    [Display(Name = "عنوان")] public string Title { get; set; }
    
    [Display(Name = "توضیحات کوتاه")] public string ShortDescription { get; set; }
    
    [Display(Name = "شرایط کاری")] public JobConditionsStatus JobConditions { get; set; }
}