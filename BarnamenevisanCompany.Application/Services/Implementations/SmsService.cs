using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.SmsProvider;
using BarnamenevisanCompany.Domain.Shared;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PARSGREEN.RESTful.SMS;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class SmsService(
    ISmsProviderRepository smsProviderRepository,
    ILogger<SmsService> logger) : ISmsService 
{
    public async Task<Result> SendSmsAsync(string[] mobiles, string message)
    {
        var providerService = await smsProviderRepository.FirstOrDefaultAsync(s => s.IsDefault);

        if (providerService is null)
        {
            logger.LogError("Get default sms provider failed. message: " + ErrorMessages.NotFoundError);
            return Result.Failure(ErrorMessages.NotFoundError);
        }

        try
        {
            switch (providerService.Type)
            {
                case SmsProviderType.ParsGreen:
                    await ParsGreenSendSms(mobiles, message);
                    break;
            }

            return Result.Success(SuccessMessages.SmsSentSuccessfully);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"sms sending failed.");
            return Result.Failure(ErrorMessages.SmsDidNotSendError);
        }
    }

    public Task<Result> SendSmsAsync(string mobile, string message) =>
        SendSmsAsync(new[] { mobile }, message);

    #region Helpers

    private async Task ParsGreenSendSms(string[] mobiles, string textMessage)
    {
        var parsGreenProvider =
            await smsProviderRepository.FirstOrDefaultAsync(s =>
                s.IsDefault && s.Type == SmsProviderType.ParsGreen);
        if (parsGreenProvider is null)
            throw new Exception("pars green provider not found!!!");
        
        var message = new Message(parsGreenProvider.ApiKey);

        var parsGreenResult = message.SendSms(
            $"{textMessage}", mobiles);

        if (parsGreenResult is not null && parsGreenResult.R_Success) return;
        string errorMessage =
            $"sms sending failed. ParsGreen response: {JsonConvert.SerializeObject(parsGreenResult)}";
        logger.LogWarning(errorMessage);
        throw new Exception(errorMessage);
    }

    #endregion
}