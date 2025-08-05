using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using BarnamenevisanCompany.Application.Statics;
using CsvHelper;
using OfficeOpenXml;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Extensions;

public static class FileGeneratorExtensions
{
    public static byte[] GenerateExcel<T>(this IEnumerable<T> data)
    {
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

        // Get the properties of the generic type
        var properties = typeof(T).GetProperties();

        // Add headers to the worksheet using display name
        for (int i = 0; i < properties.Length; i++)
        {
            string header = GetPropertyName(properties[i]);
            worksheet.Cells[1, i + 1].Value = header;
        }

        // Add data rows to the worksheet
        int rowIndex = 2;
        foreach (var item in data)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[rowIndex, i + 1].Value = properties[i].GetValue(item);
            }

            rowIndex++;
        }

        worksheet.Cells.AutoFitColumns();
        worksheet.View.RightToLeft = true;

        return package.GetAsByteArray();
    }

    public static byte[] GeneratePdf<T>(this IEnumerable<T> data, string? fontPath = null)
    {
        fontPath ??= FileGeneratorStatics.PdfFont;

        using MemoryStream memoryStream = new MemoryStream();

        // Create a new PDF document
        PdfDocument pdfDoc = new PdfDocument(new PdfWriter(memoryStream));
        Document document = new Document(pdfDoc);
        var properties = typeof(T).GetProperties();

        // Calculate document width based on the number of properties
        float docWidth = 0;

        for (int i = 0; i <= properties.Length; i++)
        {
            docWidth += 100f;
        }

        if (docWidth < 14400 && docWidth > 595)
        {
            pdfDoc.SetDefaultPageSize(new iText.Kernel.Geom.PageSize(docWidth, 842));
        }

        if (docWidth >= 14400)
        {
            pdfDoc.SetDefaultPageSize(new iText.Kernel.Geom.PageSize(14400, 842));
        }

        // Load the font
        PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H,
            PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);

        // Create a table with the number of columns equal to the number of properties
        Table table = new Table(UnitValue.CreatePercentArray(properties.Length)).UseAllAvailableWidth();

        // Set table headers
        foreach (var property in properties)
        {
            string headerText = GetPropertyName(property);
            Cell headerCell = new Cell().Add(new Paragraph(headerText).SetFont(font).SetFontSize(12));
            headerCell.SetPadding(5);
            headerCell.SetPaddingBottom(20);
            table.AddHeaderCell(headerCell);
        }

        // Set table rows
        foreach (var item in data)
        {
            foreach (var property in properties)
            {
                var cellValue = property.GetValue(item)?.ToString() ?? "";
                Cell dataCell = new Cell().Add(new Paragraph(cellValue).SetFont(font).SetFontSize(12));
                dataCell.SetPadding(5);
                dataCell.SetPaddingBottom(20);
                table.AddCell(dataCell);
            }
        }

        // Add the table to the document
        document.Add(table);

        // Close the document
        document.Close();

        // Return the generated PDF file as a byte array
        return memoryStream.ToArray();
    }

    public static byte[] GenerateCsv<T>(this IEnumerable<T> data)
    {
        using MemoryStream memoryStream = new();
        using var writer = new StreamWriter(memoryStream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties)
        {
            csv.WriteField(GetPropertyName(propertyInfo));
        }

        csv.NextRecord();

        foreach (T item in data)
        {
            foreach (var property in typeof(T).GetProperties())
            {
                csv.WriteField(property.GetValue(item)?.ToString()?.Replace(",", "-"));
            }

            csv.NextRecord();
        }

        writer.Flush();
        return memoryStream.ToArray();
    }

    public static string ConvertSvgFileToCode(this string svg, string filePath)
    {
        var result = File.ReadAllText(Directory.GetCurrentDirectory() + "/wwwroot" + filePath + svg);
        return result;
    }

    private static string GetPropertyName(PropertyInfo property)
    {
        var displayNameAttribute = property.GetCustomAttribute<DisplayNameAttribute>();
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
        return displayNameAttribute?.DisplayName ?? displayAttribute?.GetName() ?? property.Name;
    }
}