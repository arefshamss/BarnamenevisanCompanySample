using System.Text.RegularExpressions;

namespace BarnamenevisanCompany.Application.Extensions;

public static class TextExtension
{
    public static double CountWords(this string str)
    {
        var plainText = Regex.Replace(str, "<.*?>", string.Empty);
        return Regex.Matches(plainText, @"[\w-]+").Count;
    }

    public static double CountMinutesRead(this string str)
    {
        return Convert.ToInt32(str.CountWords() / 228);
    }

    public static string ConvertToMinutesForShow(this double minutes)
    {
        var timeSpan = TimeSpan.FromMinutes(minutes);

        if (timeSpan.Minutes < 1) return "زیر 1 دقیقه";

        var result = timeSpan.Hours > 0 ? timeSpan.Hours + " ساعت و " + timeSpan.Minutes + " دقیقه" 
            : timeSpan.Minutes + " دقیقه";

        return result;
    }

    public static string? CalculateReadTime(this string? text)
    {
        if (text is null) return string.Empty;

        var result = text.CountMinutesRead().ConvertToMinutesForShow();

        return result;
    }

    public static string StripHtml(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var strippedText = Regex.Replace(input, @"<(.|\n)*?>", string.Empty).Trim();
        
        if (string.IsNullOrEmpty(strippedText) || strippedText == "&nbsp;")
            return null;

        return strippedText;
    }

}