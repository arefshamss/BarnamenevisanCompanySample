namespace BarnamenevisanCompany.Application.Extensions;

public static class FileOperatorExtensions
{
    public static void Copy(string input, string output)
    {
        if (Exists(@input))
        {
            File.Copy(@input, @output, true);
            while (new FileInfo(@input).Length > new FileInfo(@output).Length)

                Thread.Sleep(1000);
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    public static void Cut(string input, string output)
    {
        if (Exists(@input))
            File.Move(@input, @output, true);
    }

    public static void Delete(string input)
    {
        if (Exists(@input))
            File.Delete(@input);
    }

    public static bool Exists(string input)
    {
        return File.Exists(@input);
    }
}