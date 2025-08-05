using BarnamenevisanCompany.Domain.Models.AboutUs;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class AboutUsSeeds
{
    public static List<AboutUs> AboutUs { get; } =    
        new()
        {
            new()
            {
                Id = 1,
                TopTitle = "برنامه‌نویسان پیشرو رسام",
                TopDescription = "کسب و کار ما توسط ارزش‌های اصلی ما هدایت می‌شود",
                MainDescriptionLeft = "مشتری از پیگیری بسیار خوشحال است. اکنون سیاست پولوینار وجود ندارد. این دارایی واقعی کازینو است. زخم های انباشته شده باعث تولد روتوم موریس می شود، اما این گیاه زنده مانده است. ",
                MainDescriptionRight = "بیش از 10 سال تجربه در این صنعت",
                OurValuesTitle = "کسب و کار ما توسط ارزش های اصلی ما هدایت می شود\n",
                OurValuesDescription = "ارزش ما\nفلسفه ای که زیربنای سازمان ما است.\n\nتا همین اواخر، دیدگاه غالب فرض می‌کرد که لورم ایپسوم به‌عنوان یک متن بی‌معنی متولد شده است.\n\nردیاب درآمد و هزینه\nصورتحساب خودکار\nاتصال کریپتو ",
                OurPassionDescription = "اشتیاق ما در تیم برنامه‌نویسان",
                TransparencyDescription = "شفافیت کاری ما در تیم برنامه‌نویسان",
                OurMissionDescription = "ماموریت ما در تیم برنامه‌نویسان",
                CreatedDate = SeedStaticDateTime.Date,
                
            }
        };
}   