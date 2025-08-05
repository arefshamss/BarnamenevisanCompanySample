using BarnamenevisanCompany.Domain.Models.Blog;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;

namespace BarnamenevisanCompany.Application.Mappers.BlogMappings;

public static class BlogMapper
{
    #region Admin

    public static AdminBlogViewModel MapToAdminBlogViewModel(this Blog model) =>
        new()
        {
            Id = model.Id,
            Author = model.User?.FirstName + " " + model.User?.LastName,
            Title = model.Title,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted,
        };

    public static Blog MapToBlog(this AdminCreateBlogViewModel model) =>
        new()
        {
            Title = model.Title,
            Description = model.Description,
            ShortDescription = model.ShortDescription,
            Slug = model.Slug.Trim().ToLower(),
            UserId = model.UserId,
        };

    public static AdminUpdateBlogViewModel MapToAdminUpdateBlogViewModel(this Blog model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            ShortDescription = model.ShortDescription,
            Slug = model.Slug.Trim().ToLower(),
            UserId = model.UserId,
            Author = model.User?.FirstName + " " + model.User?.LastName,
            ImageUrl = model.ImageUrl,
        };

    public static void UpdateBlog(this Blog blog, AdminUpdateBlogViewModel model)
    {
        blog.Title = model.Title;
        blog.Description = model.Description;
        blog.ShortDescription = model.ShortDescription;
        blog.Slug = model.Slug.Trim().ToLower();
        blog.UserId = model.UserId;
        blog.ImageUrl = model.ImageUrl;
    }

    #endregion

    #region Client

    public static ClientBlogDetailsViewModel MapToClientBlogDetailsViewModel(this Blog model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            ShortDescription = model.ShortDescription,
            ImageUrl = model.ImageUrl,
            Author = model.UserId is null ? "برنامه نویسان" : model.User?.FirstName + " " + model.User?.LastName,
            CreatedDate = model.CreatedDate,
            VisitCount = model.BlogUserMappings.Count,
        };

    public static ClientBlogViewModel MapToClientBlogViewModel(this Blog model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Author = model.UserId is null ? "برنامه نویسان" : model.User?.FirstName + " " + model.User?.LastName,
            CreatedDate = model.CreatedDate,
            VisitCount = model.BlogUserMappings.Count,
            ImageUrl = model.ImageUrl,
            ShortDescription = model.ShortDescription
        };

    #endregion
}