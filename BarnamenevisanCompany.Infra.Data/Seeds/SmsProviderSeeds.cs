using BarnamenevisanCompany.Domain.Enums.SmsProvider;
using BarnamenevisanCompany.Domain.Models.SmsProvider;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class SmsProviderSeeds
{
    public static List<SmsProvider> SmsProviders { get; } =
    [
        new()
        {
            Id = 1,
            ApiKey = "********-****-****-****-*************",
            Type = SmsProviderType.ParsGreen,
            IsDefault = true,
            CreatedDate = SeedStaticDateTime.Date
        }
    ];
}