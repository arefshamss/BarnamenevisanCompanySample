using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface ISmsService
{
    Task<Result> SendSmsAsync(string[] mobile, string message); 
    Task<Result> SendSmsAsync(string mobile, string message); 
}