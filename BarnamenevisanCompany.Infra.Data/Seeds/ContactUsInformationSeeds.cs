using BarnamenevisanCompany.Domain.Models.ContactUs;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class ContactUsInformationSeeds
{
    public static List<ContactUsInformation> ContactUsInformation { get; } =    
        new()
        {
            new()
            {
                Id = 1,
                Address = "تهران – خیابان شریعتی – خیابان ملک – بن بست ایرانیاد – پلاک ",
                Email = "barnamenevisan@info.com",
                Managment = "<p>نام مدیر مجموعه :ایمان مدائنی</p><p>&nbsp;ایمیل <a href=\":Iman@gmail.com\">:Iman@gmail.com</a></p><p>&nbsp;شماره تماس :09126700311</p> ",
                Latitude = "35.7180986994313",
                Longitude = "51.44034609553018",
                CreatedDate = SeedStaticDateTime.Date,
                PhoneNumber = "02188454816"
            }
        };
}
