using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IUserPositionService
{
    Task<AdminFilterUserPositionViewModel> FilterAsync(AdminFilterUserPositionViewModel filter,short? projectId);
    
    Task<Result> CreateAsync(AdminCreateUserPositionViewModel model);
    
    Task<Result> UpdateAsync(AdminUpdateUserPositionViewModel model);
    
    Task<Result> DeleteAsync(short id);
    
    Task<Result<AdminUpdateUserPositionViewModel>> FillModelForUpdateAsync(short id);
    
    Task<ClientFilterUserPositionViewModel> GetAllClientUserPositionsAsync(ClientFilterUserPositionViewModel filter);
    
    Task<Result<ClientUserPositionViewModel>> GetClientUserPositionByIdAsync(short id);
    
    Task<Result<int>> GetUserPositionsCountAsync();
    
    Task<bool> IsUserProgrammer(int userId);


    
}