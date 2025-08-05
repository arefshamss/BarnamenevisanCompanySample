using BarnamenevisanCompany.Application.DTOs;
using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IEmailSenderService
{


     Task<Result> SendAsync(SendMailRequestDto mailRequest);


}