using Microsoft.AspNetCore.Razor.TagHelpers;
using BarnamenevisanCompany.Application.Extensions;

namespace BarnamenevisanCompany.Web.Helpers.TagHelpers;

[HtmlTargetElement("a", Attributes = AjaxAttributeName)]
[HtmlTargetElement("form", Attributes = AjaxAttributeName)]
public class AjaxTagHelper : TagHelper
{
    #region AttributeNames

    private const string AjaxAttributeName = "asp-ajax";
    private const string AjaxValidationAttributeName = "asp-ajax-validation";
    private const string AjaxMethodAttributeName = "asp-ajax-method";
    private const string AjaxUpdateAttributeName = "asp-ajax-update";
    private const string AjaxOnSuccessAttributeName = "asp-ajax-success";
    private const string AjaxOnFailureAttributeName = "asp-ajax-failure";
    private const string AjaxOnBeginAttributeName = "asp-ajax-begin";
    private const string AjaxModeAttributeName = "asp-ajax-mode";
    private const string AjaxUrlAttributeName = "asp-ajax-url";
    private const string AjaxModalTitleAttributeName = "asp-ajax-modal-title";
    private const string AjaxModalTypeAttributeName = "asp-ajax-modal-type";
    private const string AjaxModalIndexAttributeName = "asp-ajax-modal-index";
    private const string AjaxConfirmableAttributeName = "asp-ajax-confirmable";
    private const string AjaxConfirmableTitleAttributeName = "asp-ajax-confirmable-title";
    private const string AjaxConfirmableMessageAttributeName = "asp-ajax-confirmable-message";
    private const string AjaxReinitializeAttributeName = "asp-ajax-reinitialize";
    private const string AjaxUploadAttributeName = "asp-ajax-upload";
    private const string AjaxCloseModalOnSuccessAttributeName = "asp-ajax-close-modal-onSuccess";
    private const string AjaxSubstitutionAttributeName = "asp-ajax-substitution";
    private const string AjaxCompleteAttributeName = "asp-ajax-complete";
    private const string AjaxRedirectAttributeName = "asp-ajax-redirect";
    private const string AjaxRedirectTimeOutAttributeName = "asp-ajax-redirect-timeout";

    #endregion

    #region Attributes

    [HtmlAttributeName(AjaxValidationAttributeName)]
    public bool ValidateForm { get; set; }

    [HtmlAttributeName(AjaxAttributeName)] public bool IsActive { get; set; }

    [HtmlAttributeName(AjaxMethodAttributeName)]
    public AjaxMethod Method { get; set; } = AjaxMethod.Get;

    [HtmlAttributeName(AjaxUpdateAttributeName)]
    public string? UpdateTarget { get; set; }

    [HtmlAttributeName(AjaxOnSuccessAttributeName)]
    public string? OnSuccess { get; set; }

    [HtmlAttributeName(AjaxOnFailureAttributeName)]
    public string? OnFailure { get; set; }

    [HtmlAttributeName(AjaxOnBeginAttributeName)]
    public string? OnBegin { get; set; }

    [HtmlAttributeName(AjaxModeAttributeName)]
    public AjaxMode Mode { get; set; } = AjaxMode.Replace;

    [HtmlAttributeName(AjaxUrlAttributeName)]
    public string? Url { get; set; }


    [HtmlAttributeName(AjaxModalTitleAttributeName)]
    public string? ModalTitle { get; set; }

    [HtmlAttributeName(AjaxModalTypeAttributeName)]
    public ModalType ModalType { get; set; } = ModalType.None;

    [HtmlAttributeName(AjaxModalIndexAttributeName)]
    public int ModalIndex { get; set; } = 1;


    [HtmlAttributeName(AjaxConfirmableAttributeName)]
    public bool Confirmable { get; set; }

    [HtmlAttributeName(AjaxConfirmableTitleAttributeName)]
    public string? ConfirmableTitle { get; set; }

    [HtmlAttributeName(AjaxConfirmableMessageAttributeName)]
    public string? ConfirmableMessage { get; set; }


    [HtmlAttributeName(AjaxReinitializeAttributeName)]
    public bool ReinitializeComponents { get; set; } = true;

    [HtmlAttributeName(AjaxUploadAttributeName)]
    public bool AjaxUpload { get; set; }

    [HtmlAttributeName(AjaxCloseModalOnSuccessAttributeName)]
    public CloseModalType CloseModalOnSuccess { get; set; } = CloseModalType.None;

    [HtmlAttributeName(AjaxSubstitutionAttributeName)]
    public string AjaxSubstitution { get; set; }

    [HtmlAttributeName(AjaxCompleteAttributeName)]
    public string OnComplete { get; set; }

    [HtmlAttributeName(AjaxRedirectAttributeName)]
    public string AjaxRedirect { get; set; }

    [HtmlAttributeName(AjaxRedirectTimeOutAttributeName)]

    public string AjaxRedirectTimeout { get; set; }

    #endregion

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!IsActive) return;
        if (output.TagName.ToLower().Trim() == "button")
        {
            output.Attributes.SetAttribute("type", "button");
        }

        #region On Complete

        string completeMethods = null;

        if (!string.IsNullOrWhiteSpace(OnComplete))
            completeMethods += OnComplete;

        #endregion

        output.Attributes.SetAttribute("data-ajax", "true");
        output.Attributes.SetAttribute("data-ajax-method", Method.ToString().ToLower());
        output.Attributes.SetAttribute("data-ajax-mode", Mode.GetEnumName().ToLower());
        string targetModalBody = GetModalBodyId(ModalType, ModalIndex);
        output.Attributes.SetAttribute("data-ajax-update", UpdateTarget ?? targetModalBody);
        output.Attributes.SetAttribute("data-ajax-complete", completeMethods);
        string modalFunction = ModalType switch
        {
            ModalType.Large => $"opLgModal('{ModalTitle}' , {ModalIndex});",
            ModalType.Medium => $"opModal('{ModalTitle}', {ModalIndex});",
            ModalType.Small => $"opSmModal('{ModalTitle}' , {ModalIndex});",
            ModalType.None => string.Empty,
            _ => string.Empty,
        };

        string successFunctions =
            $"{modalFunction}close_waiting();{OnSuccess}";

        #region Validation Form

        if (ValidateForm)
            successFunctions += $"validateFormByElement('{targetModalBody}');";

        #endregion

        #region Reinitialize Components

        if (ReinitializeComponents)
            successFunctions += $"reinitializeTemplateComponents('{targetModalBody}');";

        #endregion

        #region Config Confirmable

        if (Confirmable && !string.IsNullOrWhiteSpace(ConfirmableTitle) &&
            !string.IsNullOrWhiteSpace(ConfirmableMessage))
        {
            output.Attributes.SetAttribute("onclick",
                $"showConfirmableAlert(event, '{ConfirmableTitle}', '{ConfirmableMessage}')");
        }

        #endregion

        #region Config Ajax Upload

        if (AjaxUpload)
        {
            output.Attributes.SetAttribute("data-ajax-upload", "true");
            output.Attributes.SetAttribute("enctype", "multipart/form-data");
        }

        #endregion

        #region Config Close Modal On Success

        switch (CloseModalOnSuccess)
        {
            case CloseModalType.Current:
                successFunctions += "closeModalByElementParent(element);";
                break;
            case CloseModalType.All:
                successFunctions += "closeAllModals();";
                break;
        }

        #endregion

        #region Config Ajax Subsitution

        if (!AjaxSubstitution.IsNullOrEmptyOrWhiteSpace())
            successFunctions += $"ajaxSubstitutionFormId('{AjaxSubstitution}')";

        #endregion

        #region Config RedirectUrl

        if (!string.IsNullOrWhiteSpace(AjaxRedirect))
        {
            if (string.IsNullOrWhiteSpace(AjaxRedirectTimeout))
                successFunctions += $"redirect('{AjaxRedirect}');";
            else
                successFunctions += $"redirect('{AjaxRedirect}', {AjaxRedirectTimeout});";
        }

        #endregion

        output.Attributes.SetAttribute("data-ajax-success", successFunctions);
        output.Attributes.SetAttribute("data-ajax-failure", "onAjaxFailure(xhr, status, error);" + OnFailure);
        output.Attributes.SetAttribute("data-ajax-begin", 
            "open_waiting();" + CloneModalFunction(ModalType, ModalIndex) + OnBegin);
        if (Url is not null)
            output.Attributes.SetAttribute("data-ajax-url", Url);
    }

    private static string CloneModalFunction(ModalType type, int index) 
    {
        return type switch
        {
            ModalType.Large => $"cloneModal('lg' , '{index}');",
            ModalType.Medium => $"cloneModal('md' , '{index}');",
            ModalType.Small => $"cloneModal('sm' , '{index}');",
            ModalType.None => string.Empty,
            _ => string.Empty
        };
    }

    private static string GetModalBodyId(ModalType type, int index)
    {
        string targetModalBody = type switch
        {
            ModalType.Large => "#modal-center-lg-body",
            ModalType.Medium => "#modal-center-md-body",
            ModalType.Small => "#modal-center-sm-body",
            _ => string.Empty
        };

        return (index == 1) ? targetModalBody : targetModalBody + "-" + index;
    }
}

public enum AjaxMethod : byte
{
    Get,
    Post
}

public enum ModalType : byte
{
    None,
    Small,
    Medium,
    Large,
}

public enum AjaxMode : byte
{
    Replace,
    Before,
    After,
}

public enum CloseModalType : byte
{
    None,
    Current,
    All
}