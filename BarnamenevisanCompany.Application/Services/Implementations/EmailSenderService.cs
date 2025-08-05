﻿using BarnamenevisanCompany.Application.DTOs;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Shared;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class EmailSenderService(IOptions<MailSettings> settings) : IEmailSenderService
{
    private readonly string Smtp = settings.Value.Smtp;
    private readonly int Port = settings.Value.Port;
    private readonly string From = settings.Value.From;
    private readonly string UserName = settings.Value.UserName;
    private readonly string Password = settings.Value.Password;

    public async Task<Result> SendAsync(SendMailRequestDto mailRequest)
    {
        if (mailRequest is null)
            return Result.Failure(ErrorMessages.EmailSendError);
        MimeMessage message = new();
        message.From.Add(new MailboxAddress(UserName, From));
        message.To.Add(new MailboxAddress(mailRequest.To, mailRequest.To));
        message.Subject = mailRequest.Subject;
        message.Body = new TextPart("html")
        {
            Text = mailRequest.Body
        };
        try
        {
            using (SmtpClient smtpClient = new())
            {
                await smtpClient.ConnectAsync(Smtp, Port, true);
                await smtpClient.AuthenticateAsync(From, Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(ErrorMessages.EmailSendError);
        }
    }
}