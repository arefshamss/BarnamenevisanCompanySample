using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Enums.Consult;
using BarnamenevisanCompany.Domain.Models.Consult;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using BarnamenevisanCompany.Domain.ViewModels.Client;
using BarnamenevisanCompany.Domain.ViewModels.Client.Consult;
using Microsoft.IdentityModel.Tokens;

namespace BarnamenevisanCompany.Application.Mappers.ConsultMappings;

public static class ConsultMapper
{
    public static AdminConsultViewModel MapConsultViewModel(this Consult model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            CreateDate = model.CreatedDate,
            PhoneNumber = model.Mobile,
            AdminMessage = model.AdminMessage,
            IsDeleted = model.IsDeleted,
            UserFullName = model.FirstName + " " + model.LastName,
        };

    public static AdminConsultDetailViewModel MapToConsultDetailViewModel(this Consult model) =>
        new()
        {
            Id = model.Id,
            UserId = model.UserId,
            Title = model.Title,
            CreateDate = model.CreatedDate.ToShamsiDateTime(),
            AdminAnswer = model.AdminMessage,
            UserFullName = model.FirstName + " " + model.LastName,
            Description = model.Description,
            Mobile = model.Mobile,
        };

    public static Consult MapToConsult(this ClientCreateConsultViewModel model) =>
        new()
        {
            Title = model.Title,
            Description = model.Description,
            UserId = model.UserId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Mobile = model.Mobile,
        };

    public static ClientConsultListUserViewModel MapToUserPanelConsultListUserViewModel(this Consult model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ConsultStatus = model.AdminMessage.IsNullOrEmpty() ? ConsultStatus.InProcess : ConsultStatus.IsAnswered,
            CreateDate = model.CreatedDate,
        };

    public static ClientConsultDetailViewModel MaptoUserPanelConsultDetailViewModel(this Consult model) =>
        new()
        {
            Title = model.Title,
            CreateDate = model.CreatedDate,
            AdminAnswer = model.AdminMessage,
            UserFullName = model.User.FirstName + " " + model.User.LastName,
            Descriptrion = model.Description,
        };
}