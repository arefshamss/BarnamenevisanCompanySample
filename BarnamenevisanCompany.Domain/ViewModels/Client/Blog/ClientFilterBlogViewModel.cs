using System.ComponentModel.DataAnnotations;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.ViewModels.Common;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Blog;

public class ClientFilterBlogViewModel : BasePaging<ClientBlogViewModel>
{
    [FilterInput]
    public string? Title { get; set; }
}