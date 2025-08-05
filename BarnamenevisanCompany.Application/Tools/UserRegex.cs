using System.Text.RegularExpressions;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Application.Tools;

public partial class UserRegex
{
    [GeneratedRegex(SiteRegex.MobileRegex)]
    public static partial Regex MobileRegex();

    [GeneratedRegex(SiteRegex.EmailRegex)]
    public static partial Regex EmailRegex();

    [GeneratedRegex(SiteRegex.SiteUrlRegex)]
    public static partial Regex SiteUrlRegex();
}