using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.Enums.Job;

public enum JobExpirationStatus
{
    [Display(Name = "فعال")] Active,
    [Display(Name = "همه")] All,
    [Display(Name = "منقضی‌شده")] Expired
}