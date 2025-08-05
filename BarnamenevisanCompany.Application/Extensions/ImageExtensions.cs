using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace BarnamenevisanCompany.Application.Extensions;

public static class ImageExtensions
{
    public const int ImageMinimumBytes = 512;

    public static bool IsImage(this IFormFile postedFile)
    {
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        var validMimeTypes = new[] { "image/jpg", "image/jpeg", "image/png", "image/webp", "image/x-png" };
        if (!validMimeTypes.Contains(postedFile.ContentType.ToLower()))
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var fileExtension = Path.GetExtension(postedFile.FileName).ToLower();
        if (!validExtensions.Contains(fileExtension))
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            using (var stream = postedFile.OpenReadStream())
            {
                //------------------------------------------
                // Check whether the image size exceeds the limit
                //------------------------------------------
                if (stream.Length < ImageMinimumBytes)
                {
                    return false;
                }

                // Read the first bytes to check for malicious content (HTML, script, etc.)
                byte[] buffer = new byte[ImageMinimumBytes];
                stream.Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }

        //-------------------------------------------
        //  Try to load the image using ImageSharp
        //-------------------------------------------
        try
        {
            using (var stream = postedFile.OpenReadStream())
            {
                // Detect image format
                IImageFormat format = Image.DetectFormat(stream);

                // Check if the format is valid
                if (format == null || !validMimeTypes.Contains(format.DefaultMimeType.ToLower()))
                {
                    return false;
                }

                // Attempt to load the image to verify it's a valid image format
                var image = Image.Load(stream); // We no longer need to check the format here as it's already validated
            }
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static bool IsSVG(this IFormFile postedFile, bool checkImage = true, bool checkByPattern = true)
    {
        if (!checkImage) return true;
        //-------------------------------------------
        //  Check the image mime types
        //-------------------------------------------
        if (postedFile.ContentType.ToLower() != "image/svg+xml")
        {
            return false;
        }

        //-------------------------------------------
        //  Check the image extension
        //-------------------------------------------
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".svg")
        {
            return false;
        }

        //-------------------------------------------
        //  Attempt to read the file and check the first bytes
        //-------------------------------------------
        try
        {
            if (!postedFile.OpenReadStream().CanRead)
            {
                return false;
            }

            //------------------------------------------
            //check whether the image size exceeding the limit or not
            //------------------------------------------ 
            if (postedFile.Length < ImageMinimumBytes)
            {
                return false;
            }

            if (checkByPattern)
            {
                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content,
                        @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}