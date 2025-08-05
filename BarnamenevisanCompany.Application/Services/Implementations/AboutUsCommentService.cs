using BarnamenevisanCompany.Application.Mappers.AboutUsCommentMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;
using Humanizer;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class AboutUsCommentService(IAboutUsCommentRepository aboutUsCommentRepository) : IAboutUsCommentService
{
    #region FilterAsync

    public async Task<AdminFilterAboutUsCommentViewModel> FilterAsync(AdminFilterAboutUsCommentViewModel filter)
    {
        filter ??= new AdminFilterAboutUsCommentViewModel();

        var condition = Filter.GenerateConditions<AboutUsComment>();

        if (filter.Rating.HasValue)
            condition.Add(s => s.Rating == filter.Rating.Value);
        switch (filter.DeleteStatus)
        {
            case DeleteStatus.All:
                break;
            case DeleteStatus.Deleted:
                condition.Add(s => s.IsDeleted);
                break;
            case DeleteStatus.NotDeleted:
                condition.Add(s => !s.IsDeleted);
                break;
        }

        await aboutUsCommentRepository.FilterAsync(filter, condition, s => s.MapToAdminAboutUsCommentViewModel(), includes: nameof(AboutUsComment.Users));
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateAboutUsCommentViewModel model)
    {
        await aboutUsCommentRepository.InsertAsync(model.MapToAboutUsComment());
        await aboutUsCommentRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateAboutUsCommentViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var comment = await aboutUsCommentRepository.GetByIdAsync(model.Id);

        if (comment is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        comment.MapToAboutUsComment(model);

        aboutUsCommentRepository.Update(comment);
        await aboutUsCommentRepository.SaveChangesAsync();
        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateAsync

    public async Task<Result<AdminUpdateAboutUsCommentViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateAboutUsCommentViewModel>(ErrorMessages.BadRequestError);

        var comment = await aboutUsCommentRepository.GetByIdAsync(id);

        if (comment is null)
            return Result.Failure<AdminUpdateAboutUsCommentViewModel>(ErrorMessages.NotFoundError);

        return comment.MapToAdminUpdateAboutUsCommentViewModel();
    }

    #endregion

    #region DeleteOrRecover

    public async Task<Result> DeleteOrRecover(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var comment = await aboutUsCommentRepository.GetByIdAsync(id);

        if (comment is null)
            return Result.Failure(ErrorMessages.NotFoundError);
        var result = aboutUsCommentRepository.SoftDeleteOrRecover(comment);
        await aboutUsCommentRepository.SaveChangesAsync();
        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #region GetAllCommentsAsync

    public async Task<Result<List<ClientAboutUsCommentViewModel>>> GetAllCommentsAsync()
    {
        return await aboutUsCommentRepository.GetAllAsync(s => s.MapToClientAboutUsCommentViewModel(), s => !s.IsDeleted, includes: nameof(AboutUsComment.Users));
    }

    #endregion
}