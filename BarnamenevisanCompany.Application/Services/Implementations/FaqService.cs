using BarnamenevisanCompany.Application.Mappers.FaqMappings;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Enums.Common;
using BarnamenevisanCompany.Domain.Models.Faq;
using BarnamenevisanCompany.Domain.Shared;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Faq;
using BarnamenevisanCompany.Domain.ViewModels.Client.Faq;

namespace BarnamenevisanCompany.Application.Services.Implementations;

public class FaqService(
    IFaqRepository faqRepository
) : IFaqService
{
    #region Admin

    #region FilterAsync

    public async Task<Result<AdminFilterFaqViewModel>> FilterAsync(AdminFilterFaqViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Faq>();
        var orders = Filter.GenerateOrders<Faq>();

        #region Filter

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

        await faqRepository.FilterAsync(filter, conditions, x => x.MapToAdminFaqViewModel(), orders);
        return filter;
    }

    #endregion

    #region CreateAsync

    public async Task<Result> CreateAsync(AdminCreateFaqViewModel model)
    {
        var faq = model.MapToFaq();

        await faqRepository.InsertAsync(faq);
        await faqRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.InsertSuccessfullyDone);
    }

    #endregion

    #region FillModelForUpdateASync

    public async Task<Result<AdminUpdateFaqViewModel>> FillModelForUpdateAsync(short id)
    {
        if (id < 1)
            return Result.Failure<AdminUpdateFaqViewModel>(ErrorMessages.BadRequestError);

        var faq = await faqRepository.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (faq is null)
            return Result.Failure<AdminUpdateFaqViewModel>(ErrorMessages.NotFoundError);

        return faq.MapToAdminUpdateFaqViewModel();
    }

    #endregion

    #region UpdateAsync

    public async Task<Result> UpdateAsync(AdminUpdateFaqViewModel model)
    {
        if (model.Id < 1)
            return Result.Failure(ErrorMessages.BadRequestError);

        var faq = await faqRepository.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);

        if (faq is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        faq.UpdateFaq(model);

        faqRepository.Update(faq);
        await faqRepository.SaveChangesAsync();

        return Result.Success(SuccessMessages.UpdateSuccessfullyDone);
    }

    #endregion

    #region DeleteOrRecover

    public async Task<Result> DeleteOrRecoverAsync(short id)
    {
        if (id < 1)
            return Result.Failure(ErrorMessages.SomethingWentWrong);

        var faq = await faqRepository.GetByIdAsync(id);

        if (faq is null)
            return Result.Failure(ErrorMessages.NotFoundError);

        var result = faqRepository.SoftDeleteOrRecover(faq);
        await faqRepository.SaveChangesAsync();

        return result ? Result.Success(SuccessMessages.DeleteSuccess) : Result.Success(SuccessMessages.RecoverSuccess);
    }

    #endregion

    #endregion

    #region Client

    #region FilterAsync

    public async Task<Result<ClientFilterFaqViewModel>> FilterAsync(ClientFilterFaqViewModel filter)
    {
        filter ??= new();

        var conditions = Filter.GenerateConditions<Faq>();
        var orders = Filter.GenerateOrders<Faq>();

        orders.Add(x => x.CreatedDate, FilterOrderBy.Descending);
        conditions.Add(x => !x.IsDeleted);

        await faqRepository.FilterAsync(filter, conditions, x => x.MapToClientFaqViewModel(), orders);
        return filter;
    }

    #endregion

    #endregion
}