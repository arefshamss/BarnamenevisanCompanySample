namespace BarnamenevisanCompany.Application.Statics;

public static class SystemTimeZones
{
    private readonly static Dictionary<string, string> timeZones = new ()
    {
        {"fa-IR", "Iran Standard Time"},
        {"en-US", "Iran Standard Time"},
    };

    public static string GetTimeZoneStandardNameByCultureName(string cultureName)
    {
        return timeZones[cultureName];
    }

}