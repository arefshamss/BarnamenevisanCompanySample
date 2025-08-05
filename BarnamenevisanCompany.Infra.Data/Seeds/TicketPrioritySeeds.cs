using BarnamenevisanCompany.Domain.Models.Ticket;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class TicketPrioritySeeds
{
    public static List<TicketPriority> TicketPriorities { get; } =
    [
        new()
        {
            Id = 1,
            Title = "زیاد",
            CreatedDate = SeedStaticDateTime.Date
        },
        new()
        {
            Id = 2,
            Title = "متوسط",
            CreatedDate = SeedStaticDateTime.Date
        },
        new()
        {
            Id = 3,
            Title = "کم",
            CreatedDate = SeedStaticDateTime.Date
        },
    ];
}