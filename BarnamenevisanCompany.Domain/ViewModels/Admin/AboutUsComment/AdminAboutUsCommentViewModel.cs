namespace BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUsComment;

public class AdminAboutUsCommentViewModel
{
    public short Id { get; set; }
    public string FullName { get; set; }

    public byte Rating { get; set; }

    public bool IsDeleted { get; set; }
}