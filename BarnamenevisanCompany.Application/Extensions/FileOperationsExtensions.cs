namespace BarnamenevisanCompany.Application.Extensions;

public static class FileOperationsExtensions
{
    public static bool IsFileExist(this string filePath)
        => File.Exists(filePath);

    public static bool IsFilePathRar(this string filePath)
    {   
        if(!File.Exists(filePath))
            return false;

        return Path.GetExtension(filePath) == ".rar";
    }

    public static bool IsFilePathMp4(this string filePath)
    {
        if(!File.Exists(filePath))
            return false;

        return Path.GetExtension(filePath) == ".mp4";
    }
    
    public static bool IsFilePathPng(this string filePath)
    {
        if(!File.Exists(filePath))
            return false;

        return Path.GetExtension(filePath) == ".png";
    }
    public static bool IsFilePathSvg(this string filePath)
    {
        if(!File.Exists(filePath))
            return false;

        return Path.GetExtension(filePath) == ".svg";
    }
    public static bool IsFilePathJpg(this string filePath)
    {
        if(!File.Exists(filePath))
            return false;

        return Path.GetExtension(filePath) == ".jpg";
    }
}