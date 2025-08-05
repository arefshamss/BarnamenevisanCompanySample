using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.ContactUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.ContactUs;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IContactUsService
{
    Task<AdminFilterContactUsViewModel> FilterAsync(AdminFilterContactUsViewModel adminFilter);
    
    Task<Result> CreateAsync(ClientCreateContactUsViewModel model);
    
    Task<Result> DeleteOrRecoverAsync(short id);
    
    Task<Result> AnswerAsync(AdminAnswerContactUsMessageViewModel model);
    
    Task<Result<AdminAnswerContactUsMessageViewModel>> FillModelForAnswerAsync(short id);
}