using BarnamenevisanCompany.Application.DTOs;
using BarnamenevisanCompany.Application.Mappers.ContactUsMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.ContactUs;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUs;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ContactUsService(
    IContactUsRepository contactUsRepository,
    IEmailSenderService emailSenderService,
    IViewRenderService viewRenderService) : IContactUsService
{
    #region Filter

    public async Task<AdminFilterContactUsViewModel> FilterAsync(AdminFilterContactUsViewModel adminFilter)
    {
        var condition = Filter.GenerateConditions<ContactUs>();
        var order = Filter.GenerateOrders<ContactUs>();

        #region Filter

        if (!string.IsNullOrEmpty(adminFilter.Title))
            condition.Add(u => EF.Functions.Like(u.Title, $"%{adminFilter.Title}%"));

        if (!string.IsNullOrEmpty(adminFilter.Email))
            condition.Add(u => EF.Functions.Like(u.Email, $"%{adminFilter.Email}%"));

        switch (adminFilter.FilterOrderBy)
        {
            case FilterOrderBy.Ascending:
                order.Add(u => u.CreatedDate);
                break;
            case FilterOrderBy.Descending:

                break;
        }

        switch (adminFilter.DeleteStatus)
        {
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                condition.Add(u => u.IsDeleted);
                break;
            case DeleteStatus.NotDeleted:
                condition.Add(u => !u.IsDeleted);
                break;
        }

        switch (adminFilter.ContactUsStatus)
        {
            case ContactUsStatus.All:
                break;
            case ContactUsStatus.InProcess:
                condition.Add(u => u.AdminMessage == null);
                break;
            case ContactUsStatus.IsAnswered:
                condition.Add(u => u.AdminMessage != null);
                break;
        }

        #endregion

        await contactUsRepository.FilterAsync(adminFilter, condition, u => u.MapToContactUsViewModel(), order);

        return adminFilter;
    }

    #endregion

    #region Create

    public async Task<Result> CreateAsync(ClientCreateContactUsViewModel model)
    {
        var newContactUs = model.MapToContactUs();

        await contactUsRepository.InsertAsync(newContactUs);

        await contactUsRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.MessageSentSuccessfully);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var contact = await contactUsRepository.GetByIdAsync(id);

        if (contact is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = contactUsRepository.SoftDeleteOrRecover(contact);
        await contactUsRepository.SaveChangesAsync();
        if (result)
            return Result.Success(SuccessMessages.DeleteSuccess);

        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region AnswerAsync

    public async Task<Result> AnswerAsync(AdminAnswerContactUsMessageViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var contact = await contactUsRepository.GetByIdAsync(model.Id);

        if (contact is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (contact.AdminMessage is not null)
            return Result.Failure(ErrorMessages.AlreadyAnsweredThisMessage);
        
        contact.AdminMessage = model.AdminMessage;

        contactUsRepository.Update(contact);
        await contactUsRepository.SaveChangesAsync();
        
        if (contact.AdminMessage is not null)
        {
            var bodyModel = contact.MapToAdminEmailTemplateContactUsViewModel();

            var body = await viewRenderService.RenderToStringAsync("Email/ContactUs", bodyModel);
            if (!string.IsNullOrEmpty(body))
            {
                var newMail = new SendMailRequestDto
                {
                    Body = body,
                    Subject = contact.Title,
                    To = contact.Email,
                    Title = contact.Title,
                    From = "برنامه نویسان"
                };
                var result = await emailSenderService.SendAsync(newMail);
            }
        }

        return Result.Success(SuccessMessages.EmailSentSuccessfully);
    }

    #endregion

    #region FillModelForAnswer

    public async Task<Result<AdminAnswerContactUsMessageViewModel>> FillModelForAnswerAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminAnswerContactUsMessageViewModel>(ErrorMessages.BadRequestError);

        var contact = await contactUsRepository.GetByIdAsync(id);

        if (contact is null)
            return Result.Failure<AdminAnswerContactUsMessageViewModel>(ErrorMessages.NotFoundError);

        return contact.MapToAdminAnswerContactUsMessageViewModel();
    }

    #endregion
}