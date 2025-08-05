using System.Collections.Specialized;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class UserSocialNetworkSeeds
{
    public static List<UserSocialNetwork> UserSocialNetworks { get; } =
    [
        new()
        {
            Id = 1,
            Title = "Linkedin",
            ImageName = "Linkedin.svg",
            PersianTitle = "لینکدین",
            CreatedDate = SeedStaticDateTime.Date,
        },
        new()
        {
            Id = 2,
            Title = "Instagram",
            ImageName = "Instagram.svg",
            PersianTitle = "اینستاگرام",
            CreatedDate = SeedStaticDateTime.Date,
        },
        new()
        {
            Id = 3,
            Title = "Telegram",
            ImageName = "Telegram.svg",
            PersianTitle = "تلگرام",
            CreatedDate = SeedStaticDateTime.Date,
        },
        new()
        {
            Id = 4,
            Title = "Github",
            ImageName = "GitHub.svg",
            PersianTitle = "گیت هاب",
            CreatedDate = SeedStaticDateTime.Date,
        },
        new()
        {
            Id = 5,
            Title = "Gmail",
            ImageName = "Gmail.svg",
            PersianTitle = "ایمیل",
            CreatedDate = SeedStaticDateTime.Date,
        }
    ];
}