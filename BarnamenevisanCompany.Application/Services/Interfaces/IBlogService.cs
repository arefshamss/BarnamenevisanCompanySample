using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IBlogService
{
    #region Admin

    Task<Result<AdminFilterBlogViewModel>> FilterAsync(AdminFilterBlogViewModel filter);

    Task<Result> CreateAsync(AdminCreateBlogViewModel model);

    Task<Result> UpdateAsync(AdminUpdateBlogViewModel model);

    Task<Result<AdminUpdateBlogViewModel>> FillModelForUpdateAsync(int id);

    Task<Result> DeleteOrRecoverAsync(int id);

    #endregion

    #region Client

    Task<Result<ClientFilterBlogViewModel>> FilterAsync(ClientFilterBlogViewModel filter);

    Task<Result<ClientBlogDetailsViewModel>> GetBySlugAsync(string slug);

    Task<Result<string>> GetSlugByIdAsync(int id);

    #endregion
}