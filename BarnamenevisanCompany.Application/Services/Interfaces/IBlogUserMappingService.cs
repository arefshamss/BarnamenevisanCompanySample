namespace BarnamenevisanCompany.Application.Services.Interfaces;

public interface IBlogUserMappingService
{
    Task RegisterVisitAsync(int blogId, int? userId, string userIp);
}