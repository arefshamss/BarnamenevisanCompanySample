using BarnamenevisanCompany.Application.Mappers.ConsultMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Enums.Consult;
using BarnamenevisanCompany.Domain.Models.Consult;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Consult;
using BarnamenevisanCompany.Domain.ViewModels.Client;
using BarnamenevisanCompany.Domain.ViewModels.Client.Consult;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class ConsultService(
    IConsultRepository consultRepository,
    ISmsService smsService,
    IUserRepository userRepository,
    ISiteSettingRepository siteSettingRepository) : IConsultService
{
    #region FilterAsync

    public async Task<AdminFilterConsultViewModel> FilterAsync(AdminFilterConsultViewModel filter)
    {
        filter = filter ?? new AdminFilterConsultViewModel();

        var condition = Filter.GenerateConditions<Consult>();
        var order = Filter.GenerateOrders<Consult>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(u => EF.Functions.Like(u.Title, $"%{filter.Title}%"));


        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Ascending:
                order.Add(u => u.CreatedDate);
                break;
            case FilterOrderBy.Descending:
                break;
        }

        if (filter.UserId > 0)
            condition.Add(x => x.UserId == filter.UserId);

        switch (filter.DeleteStatus)
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

        switch (filter.ConsultStatus)
        {
            case ConsultStatus.All:
                break;
            case ConsultStatus.InProcess:
                condition.Add(u => u.AdminMessage == null);
                break;
            case ConsultStatus.IsAnswered:
                condition.Add(u => u.AdminMessage != null);
                break;
        }

        #endregion

        await consultRepository.FilterAsync(filter, condition, u => u.MapConsultViewModel(), order,
            includes: nameof(Consult.User));
        return filter;
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var consult = await consultRepository.GetByIdAsync(id);

        if (consult is null)
            return Result.Failure(ErrorMessages.NotFoundError);


        var res = consultRepository.SoftDeleteOrRecover(consult);

        await consultRepository.SaveChangesAsync();

        if (res)
            return Result.Success(SuccessMessages.DeleteSuccess);

        return Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region FillModelDetailForShow

    public async Task<Result<AdminConsultDetailViewModel>> FillModelDetailForShow(short id)
    {
        if (id < 1)
            return Result.Failure<AdminConsultDetailViewModel>(ErrorMessages.BadRequestError);

        var consult = await consultRepository.FirstOrDefaultAsync(u => u.Id == id, includes: nameof(Consult.User));

        if (consult is null)
            return Result.Failure<AdminConsultDetailViewModel>(ErrorMessages.NotFoundError);


        return consult.MapToConsultDetailViewModel();
    }

    #endregion

    #region AnswerAsync

    public async Task<Result> AnswerAsync(AdminAnswerConsultViewModel model)
    {
        if (model.ConsultId < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var consult = await consultRepository.FirstOrDefaultAsync(s => s.Id == model.ConsultId && !s.IsDeleted,
            includes: nameof(Consult.User));

        if (consult is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        if (consult.AdminMessage == null && consult.UserId is not null)
        {
            var smsResult = await SendSmsForAnswerAsync(consult.Mobile,
                string.Format(SmsMessages.ConsultAnswer, consult.FirstName, consult.LastName, consult.Title));
            if (smsResult.IsFailure)
            {
                return Result.Failure(smsResult.Message);
            }

            consult.AdminMessage = model.AdminMessage;

            consultRepository.Update(consult);
            await consultRepository.SaveChangesAsync();

            return Result.Success(SuccessMessages.ConsultSmsSentSuccessfully);
        }

        consult.AdminMessage = model.AdminMessage;

        consultRepository.Update(consult);
        await consultRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region SendSms

    public async Task<Result> SendSmsForAnswerAsync(string phoneNumber, string message)
    {
        Result smsSendResult;
        byte retryCount = 0;
        do
        {
            smsSendResult = await smsService.SendSmsAsync(phoneNumber, message);
            retryCount++;
        } while (smsSendResult.IsFailure && retryCount <= 5);

        return smsSendResult.IsSuccess
            ? Result.Success(message: SuccessMessages.ConsultSmsSentSuccessfully)
            : Result.Failure(message: ErrorMessages.SmsDidNotSendError);
    }

    #endregion


    #region CreateAsync

    public async Task<Result> CreateAsync(ClientCreateConsultViewModel model)
    {
        if (model.UserId is not null &&
            !await userRepository.AnyAsync(x => x.Id == model.UserId && x.IsActive && !x.IsDeleted))
            return Result.Failure(ErrorMessages.NotFoundError);


        var siteSettings = await siteSettingRepository.FirstOrDefaultAsync();
        if (siteSettings == null)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var consultSmsMessage = siteSettings.ConsultSmsMessage;

        var userMessage = string.Format(consultSmsMessage, $"{model.FirstName} {model.LastName}", model.Title);
        var smsResult = await SendSmsForAnswerAsync(model.Mobile, userMessage);
        if (smsResult.IsFailure)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        if (!string.IsNullOrWhiteSpace(siteSettings.NotificationPhoneNumber))
        {
            var adminMessage =
                $"یک درخواست مشاوره جدید با عنوان «{model.Title}» از طرف {model.FirstName} {model.LastName} ثبت شده است.";
            var notifyAdminResult = await SendSmsForAnswerAsync(siteSettings.NotificationPhoneNumber, adminMessage);

            if (notifyAdminResult.IsFailure)
                return Result.Failure(ErrorMessages.SomethingWentWrong);
        }

        await consultRepository.InsertAsync(model.MapToConsult());
        await consultRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertConsultSuccessfully);
    }

    #endregion

    #region GetPageInformation

    public async Task<Result<string>> GetPageInformation(byte id)
    {
        if (id < 1)
            return Result.Failure<string>(ErrorMessages.BadRequestError);

        var information = await siteSettingRepository.GetByIdAsync(id);

        if (information is null)
            return Result.Failure<string>(ErrorMessages.NotFoundError);

        return information.ConsultInformationText;
    }

    #endregion

    #region UserpanelFilter

    public async Task<Result<ClientConsultFilterViewModel>> FilterAsync(ClientConsultFilterViewModel filter, int userId)
    {
        var condition = Filter.GenerateConditions<Consult>();

        condition.Add(s => s.UserId == userId);

        if (!string.IsNullOrEmpty(filter.Title))
            condition.Add(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

        await consultRepository.FilterAsync(filter, condition, s => s.MapToUserPanelConsultListUserViewModel());
        return filter;
    }

    #endregion

    #region GetConsultDetail

    public async Task<Result<ClientConsultDetailViewModel>> GetConsultDetailAsync(short id)
    {
        if (id < 1)
            return Result.Failure<ClientConsultDetailViewModel>(ErrorMessages.BadRequestError);

        var consult = await consultRepository.FirstOrDefaultAsync(s => s.Id == id, includes: nameof(Consult.User));

        if (consult is null)
            return Result.Failure<ClientConsultDetailViewModel>(ErrorMessages.NotFoundError);

        return consult.MaptoUserPanelConsultDetailViewModel();
    }

    #endregion
}