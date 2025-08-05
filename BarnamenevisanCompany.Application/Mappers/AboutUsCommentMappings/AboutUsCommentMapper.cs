using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;
using Microsoft.IdentityModel.Tokens;

namespace BarnamenevisanCompany.Application.Mappers.AboutUsCommentMappings;

public static class AboutUsCommentMapper
{
    public static AboutUsComment MapToAboutUsComment(this AdminCreateAboutUsCommentViewModel model) =>
        new()
        {
            Comment = model.Comment,
            UserId = model.UserId,
            Rating = model.Rating,
        };

    public static AdminUpdateAboutUsCommentViewModel MapToAdminUpdateAboutUsCommentViewModel(this AboutUsComment model) =>
        new()
        {
            Comment = model.Comment,
            UserId = model.UserId,
            Rating = model.Rating,
            Id = model.Id,
        };

    public static void MapToAboutUsComment(this AboutUsComment model, AdminUpdateAboutUsCommentViewModel viewModel)
    {
        model.Comment = viewModel.Comment;
        model.UserId = viewModel.UserId;
        model.Rating = viewModel.Rating;
        model.Id = viewModel.Id;
    }

    public static AdminAboutUsCommentViewModel MapToAdminAboutUsCommentViewModel(this AboutUsComment model) =>
        new()
        {
            FullName = model.Users.FirstName.IsNullOrEmpty() ? model.Users.Mobile : model.Users.FirstName + " " + model.Users.LastName,
            Id = model.Id,
            Rating = model.Rating,
            IsDeleted = model.IsDeleted,
        };

    public static ClientAboutUsCommentViewModel MapToClientAboutUsCommentViewModel(this AboutUsComment model) =>
        new()
        {
            Comment = model.Comment,
            Rating = model.Rating,
            FullName = model.Users.FirstName.IsNullOrEmpty() ? "کاربر برنامه نویسان" : model.Users.FirstName + " " + model.Users.LastName,
        };
}