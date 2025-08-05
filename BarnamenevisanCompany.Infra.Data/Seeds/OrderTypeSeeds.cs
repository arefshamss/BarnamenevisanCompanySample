using BarnamenevisanCompany.Domain.Models.Order;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class OrderTypeSeeds
{
    public static List<OrderType> OrderTypes { get; } =
        [
            new()
            {
                Id = 1,
                Title = "وب",
                ImageUrl = "OrderType-Web.png",
                CreatedDate = SeedStaticDateTime.Date,
            },
            new()
            {
                Id = 2,
                Title = "دسکتاپ",
                ImageUrl = "OrderType-Desktop.png",
                CreatedDate = SeedStaticDateTime.Date,
            },
            new()
            {
                Id = 3,
                Title = "موبایل",
                ImageUrl = "OrderType-Mobile.png",
                CreatedDate = SeedStaticDateTime.Date,
            }
        ];
}