using BarnamenevisanCompany.Domain.Shared;

namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IProjectVisitsMappingService
{
    Task<Result> RegisterProjectVisitAsync(short projectId, string userIp,int? userId = null);
}