using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IAboutUsCommentService
{
    Task<AdminFilterAboutUsCommentViewModel> FilterAsync(AdminFilterAboutUsCommentViewModel filter);
    Task<Result> CreateAsync(AdminCreateAboutUsCommentViewModel model);
    
    Task<Result> UpdateAsync(AdminUpdateAboutUsCommentViewModel model);
    
    Task<Result<AdminUpdateAboutUsCommentViewModel>> FillModelForUpdateAsync(short id);
    
    Task<Result> DeleteOrRecover(short id);
    
    Task<Result<List<ClientAboutUsCommentViewModel>>> GetAllCommentsAsync();
}