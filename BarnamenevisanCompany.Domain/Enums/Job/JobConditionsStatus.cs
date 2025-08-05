using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Job;

public enum JobConditionsStatus
{
    [Display(Name="تمام‌وقت")]
    FullTime,
    [Display(Name="پاره‌وقت")]
    PartTime,
    [Display(Name="دورکاری")]
    Remote,
}

public enum FilterJobConditionsStatus
{
    [Display(Name="همه")]
    All,
    [Display(Name="تمام‌وقت")]
    FullTime,
    [Display(Name="پاره‌وقت")]
    PartTime,
}