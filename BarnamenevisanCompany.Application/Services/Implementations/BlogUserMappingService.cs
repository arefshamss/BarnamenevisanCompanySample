using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Blog;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class BlogUserMappingService(
    IBlogUserMappingRepository blogUserMappingRepository
) : IBlogUserMappingService
{
    public async Task RegisterVisitAsync(int blogId, int? userId, string userIp)
    {
        var result = await blogUserMappingRepository.AnyAsync(x => x.BlogId == blogId && x.UserId == userId && x.UserIp == userIp);

        if (!result)
        {
            var blogUserMapping = new BlogUserMapping
            {
                BlogId = blogId,
                UserId = userId,
                UserIp = userIp
            };

            await blogUserMappingRepository.InsertAsync(blogUserMapping);
            await blogUserMappingRepository.SaveChangesAsync();
        }
    }
}