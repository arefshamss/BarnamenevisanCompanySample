using System.Security.Claims;
using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Application.Mappers.BlogMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Blog;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Blog;
using BarnamenevisanCompany.Domain.ViewModels.Client.Blog;
using BarnamenevisanCompany.Domain.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class BlogService(
    IBlogRepository blogRepository
) : IBlogService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterBlogViewModel>> FilterAsync(AdminFilterBlogViewModel filter)
    {
        filter??=new();

        var conditions = Filter.GenerateConditions<Blog>();
        var orders = Filter.GenerateOrders<Blog>();

        #region Filter

        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title.Trim()}%"));

        if (filter.UserId > 0)
            conditions.Add(x => x.UserId == filter.UserId);

        if (!filter.FromDate.IsNullOrEmptyOrWhiteSpace())
            conditions.Add(s => s.CreatedDate >= filter.FromDate!.ToMiladiDateTime());

        if (!filter.ToDate.IsNullOrEmptyOrWhiteSpace())
            conditions.Add(s => s.CreatedDate <= filter.ToDate!.ToMiladiDateTime());

        switch (filter.DeleteStatus)
        {
            case DeleteStatus.NotDeleted:
                conditions.Add(x => !x.IsDeleted);
                break;
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                conditions.Add(x => x.IsDeleted);
                break;
        }

        switch (filter.FilterOrderBy)
        {
            case FilterOrderBy.Descending:
                orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
                break;

            case FilterOrderBy.Ascending:
                orders.Add(x => x.CreatedDate);
                break;
        }

        #endregion

        await blogRepository.FilterAsync(filter, conditions, x => x.MapToAdminBlogViewModel(), orders, includes: nameof(Blog.User));
        return filter;
    }
    

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateBlogViewModel model)
    {
        #region Validations

        if (await blogRepository.AnyAsync(x => x.Slug == model.Slug))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "آدرس صفحه"));

        #endregion

        var blog = model.MapToBlog();

        #region Add Image
        var guidFileName = Guid.NewGuid().ToString();
        var result = await model.Image.AddImageToServer(FilePaths.BlogOriginalImage, 384, 269, FilePaths.BlogThumbImage, suggestedFileName: guidFileName);
        if (result.IsFailure)
            return Result.Failure(result.Message);
        blog.ImageUrl = result.Value;

        #endregion

        await blogRepository.InsertAsync(blog);
        await blogRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateBlogViewModel>> FillModelForUpdateAsync(int id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateBlogViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(Blog.User),
        ];

        var blog = await blogRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: includes);

        if (blog is null)
            return Result.Failure<AdminUpdateBlogViewModel>(ErrorMessages.NotFoundError);

        return blog.MapToAdminUpdateBlogViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateBlogViewModel model)
    {
        #region Validations

        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        if (await blogRepository.AnyAsync(x => x.Slug == model.Slug && x.Id != model.Id))
            return Result.Failure(string.Format(ErrorMessages.AlreadyExistError, "آدرس صفحه"));

        #endregion


        var blog = await blogRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (blog is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        blog.UpdateBlog(model);

        if (model.Image is not null)
        {
            var result = await model.Image.AddImageToServer(FilePaths.BlogOriginalImage,  384, 269, FilePaths.BlogThumbImage, deleteFileName: blog.ImageUrl);
            if (result.IsFailure)
                return Result.Failure(result.Message);
            blog.ImageUrl = result.Value;
        }

        blogRepository.Update(blog);
        await blogRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecoverAsync

    public async Task<Result> DeleteOrRecoverAsync(int id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var blog = await blogRepository.GetByIdAsync(id);

        if (blog is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = blogRepository.SoftDeleteOrRecover(blog);
        await blogRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

#endregion

    #endregion

    #region Client

    #region FilterAsync

    public async Task<Result<ClientFilterBlogViewModel>> FilterAsync(ClientFilterBlogViewModel filter)
    {
        filter??=new();

        var conditions = Filter.GenerateConditions<Blog>();
        var orders = Filter.GenerateOrders<Blog>();

        conditions.Add(x => !x.IsDeleted);

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
        
        if (!string.IsNullOrEmpty(filter.Title))
            conditions.Add(x => EF.Functions.Like(x.Title, $"%{filter.Title.Trim()}%"));

        string[] includes =
        [
            nameof(Blog.User),
            nameof(Blog.BlogUserMappings)
        ];

        await blogRepository.FilterAsync(filter, conditions, x => x.MapToClientBlogViewModel(), orders, includes: includes);
        return filter;
    }

    #endregion

    #region GetBySlugAsync

    public async Task<Result<ClientBlogDetailsViewModel>> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            return Result.Failure<ClientBlogDetailsViewModel>(ErrorMessages.BadRequestError);

        string[] includes =
        [
            nameof(Blog.User),
            nameof(Blog.BlogUserMappings)
        ];

        var blog = await blogRepository.FirstOrDefaultAsync(x => x.Slug == slug && !x.IsDeleted, includes: includes);

        if (blog is null)
            return Result.Failure<ClientBlogDetailsViewModel>(ErrorMessages.NotFoundError);

        return blog.MapToClientBlogDetailsViewModel();
    }

    #endregion

    #region GetSlugByIdAsync

    public async Task<Result<string>> GetSlugByIdAsync(int id)
    {
        if (id < 1)
            return Result.Failure<string>(ErrorMessages.BadRequestError);

        var result = await blogRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, includes: nameof(Blog.User));

        if (result is null)
            return Result.Failure<string>(ErrorMessages.NotFoundError);

        return result.Slug;
    }

    #endregion
    
    #endregion
}