using BarnamenevisanCompany.Domain.Models.Faq;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Faq;
using BarnamenevisanCompany.Domain.ViewModels.Client.Faq;

namespace BarnamenevisanCompany.Application.Mappers.FaqMappings;

public static class FaqMapper
{
    #region Admin

    public static AdminFaqViewModel MapToAdminFaqViewModel(this Faq model) =>
        new()
        {
            Id = model.Id,
            Question = model.Question,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted
        };

    public static Faq MapToFaq(this AdminCreateFaqViewModel model) =>
        new()
        {
            Question = model.Question,
            Answer = model.Answer,
        };

    public static AdminUpdateFaqViewModel MapToAdminUpdateFaqViewModel(this Faq model) =>
        new()
        {
            Id = model.Id,
            Question = model.Question,
            Answer = model.Answer,
        };

    public static void UpdateFaq(this Faq model, AdminUpdateFaqViewModel viewModel)
    {
        model.Question = viewModel.Question;
        model.Answer = viewModel.Answer;
    }

    #endregion

    #region Client

    public static ClientFaqViewModel MapToClientFaqViewModel(this Faq model) =>
        new()
        {
            Id = model.Id,
            Question = model.Question,
            Answer = model.Answer,
        };

    #endregion
}