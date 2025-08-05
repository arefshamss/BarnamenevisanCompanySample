using BarnamenevisanCompany.Domain.ViewModels.Admin.ProjectGallery;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Technology;
using BarnamenevisanCompany.Domain.ViewModels.Admin.UserPosition;
using BarnamenevisanCompany.Domain.ViewModels.Client.UserPosition;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Project;

public class ClientProjectDetailViewModel
{
    public short Id { get; set; }

    public string Title { get; set; }

    public string Descripition { get; set; }

    public string SiteUrl { get; set; }

    public string? TeaserPoster { get; set; }


    public byte ProgressByte { get; set; }

    public string ImageName { get; set; }

    public string? TeaserName { get; set; }

    public int? Views { get; set; }

    public List<AdminTechnologyViewModel> Technologys { get; set; }

    public List<ClientProjectMemberViewModel> Programmers { get; set; }

    public List<AdminProjectGalleryViewModel> TechnologyGallery { get; set; }
}