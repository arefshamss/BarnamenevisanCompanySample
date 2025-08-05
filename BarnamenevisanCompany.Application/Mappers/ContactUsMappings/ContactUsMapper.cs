using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUs;

namespace BarnamenevisanCompany.Application.Mappers.ContactUsMappings;

public static class ContactUsMapper
{
    public static AdminContactUsViewModel MapToContactUsViewModel(this ContactUs contactUs) =>
        new()
        {
            Email = contactUs.Email,
            PhoneNumber = contactUs.PhoneNumber,
            FullName = contactUs.FullName,
            Title = contactUs.Title,
            Id = contactUs.Id,
            IsDeleted = contactUs.IsDeleted,
            CreateDate = contactUs.CreatedDate,
            AdminMessage = contactUs.AdminMessage
        };


    public static ContactUs MapToContactUs(this ClientCreateContactUsViewModel contactUs) =>
        new()
        {
            Email = contactUs.Email,
            PhoneNumber = contactUs.PhoneNumber,
            FullName = contactUs.FullName,
            Title = contactUs.Title,
            Message = contactUs.Message,
        };

    public static AdminAnswerContactUsMessageViewModel MapToAdminAnswerContactUsMessageViewModel(this ContactUs contactUs) =>
        new()
        {
            Email = contactUs.Email,
            Title = contactUs.Title,
            Id = contactUs.Id,
            UserMessage = contactUs.Message,
            AdminMessage = contactUs.AdminMessage,
            Createdate = contactUs.CreatedDate.ToShamsiDateTime()
        };

    public static AdminEmailTemplateContactUsVIewModel MapToAdminEmailTemplateContactUsViewModel(this ContactUs contactUs) =>
        new()
        {
            Title = contactUs.Title,
            AdminMessage = contactUs.AdminMessage,
            FullName = contactUs.FullName

        };
}