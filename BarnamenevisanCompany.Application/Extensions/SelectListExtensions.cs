using BarnamenevisanCompany.Domain.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarnamenevisanCompany.Application.Extensions;

public static class SelectListExtensions
{
    public static SelectList ToSelectList<TKey>(this List<SelectViewModel<TKey>> source, TKey selectedValue = default)
        => new SelectList(source.ToList(), nameof(SelectViewModel<TKey>.Id),
            nameof(SelectViewModel<TKey>.DisplayValue), selectedValue);
}