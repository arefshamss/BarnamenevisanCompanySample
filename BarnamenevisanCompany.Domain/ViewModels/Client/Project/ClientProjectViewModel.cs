namespace BarnamenevisanCompany.Domain.ViewModels.Client.Project;

public class ClientProjectViewModel
{
    public short Id { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageName { get; set; }

    public string Slug { get; set; }
    public byte ProgressRate { get; set; }

    public short Priority { get; set; }
}