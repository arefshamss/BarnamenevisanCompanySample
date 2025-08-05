using System.ComponentModel.DataAnnotations;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.ContactUsInformation;

public class ClientContactUsInformationViewModel
{
    
    public short Id { get; set; }
    [MaxLength(200)]
    [Display(Name = "ایمیل")]
    [DataType(dataType: DataType.EmailAddress)]
    public string Email { get; set; }
    [MaxLength(200)]
    [Display(Name = "مدیریت")]
    
    public string Managment { get; set; }

    [Display(Name = "تلفن")]
    public string PhoneNumber { get; set; }
    [MaxLength(500)]
    [Display(Name = "آدرس")]
    public string Address { get; set; }
   
    [Display(Name = "عرض جغرافیایی")]

    public string Latitude { get; set; }
    [Display(Name = " طول جغرافیایی")]
    public string Longitude { get; set; }
}