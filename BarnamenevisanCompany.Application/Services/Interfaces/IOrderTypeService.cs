using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.OrderType;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderType;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IOrderTypeService
{
    #region Admin

    Task<Result<AdminFilterOrderTypeViewModel>> FilterAsync(AdminFilterOrderTypeViewModel filter);

    Task<Result> CreateAsync(AdminCreateOrderTypeViewModel model);

    Task<Result<AdminUpdateOrderTypeViewModel>> FillModelForUpdateAsync(short id);

    Task<Result> UpdateAsync(AdminUpdateOrderTypeViewModel model);

    Task<Result> DeleteOrRecoverAsync(short id);

    #endregion

    #region Client

    Task<List<ClientOrderTypeViewModel>> GetAllAsync();

    Task<Result<ClientOrderTypeDetailsViewModel>> GetByIdAsync(short id);

    #endregion
}