using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Admin.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;
using BarnamenevisanCompany.Domain.ViewModels.Client.OrderStep;

namespace BarnamenevisanCompany.Application.Mappers.AboutUsMappings;

public static class AboutUsMapper
{
    public static AdminUpdateAboutUsViewModel MapToAdminAboutUsUpdateAsync(this AboutUs model) =>
        new()
        {
            MainDescriptionLeft = model.MainDescriptionLeft,
            MainDescriptionRight = model.MainDescriptionRight,
            TopDescription = model.TopDescription,
            TopTitle = model.TopTitle,
            OurMissionDescription = model.OurMissionDescription,
            TransparencyDescription = model.TransparencyDescription,
            OurPassionDescription = model.OurPassionDescription,
            OurValuesTitle = model.OurValuesTitle,
            OurValuesDescription = model.OurValuesDescription,
        };

    public static void MapToAboutUs(this AboutUs model, AdminUpdateAboutUsViewModel viewModel)
    {
        model.MainDescriptionLeft = viewModel.MainDescriptionLeft;
        model.MainDescriptionRight = viewModel.MainDescriptionRight;
        model.TopDescription = viewModel.TopDescription;
        model.TopTitle = viewModel.TopTitle;
        model.OurPassionDescription = viewModel.OurPassionDescription;
        model.OurValuesTitle = viewModel.OurValuesTitle;
        model.TransparencyDescription = viewModel.TransparencyDescription;
        model.OurValuesDescription = viewModel.OurValuesDescription;
        model.OurMissionDescription = viewModel.OurMissionDescription;
    }

    public static ClientAboutUsViewModel MapToClientAboutUsViewModel(this AboutUs model, List<ClientOrderStepViewModel> clientOrderSteps, string aboutUsTitle, string aboutUsDescription) =>
        new()
        {
            MainDescriptionLeft = model.MainDescriptionLeft,
            MainDescriptionRight = model.MainDescriptionRight,
            TopDescription = model.TopDescription,
            TopTitle = model.TopTitle,
            OurMissionDescription = model.OurMissionDescription,
            TransparencyDescription = model.TransparencyDescription,
            OurPassionDescription = model.OurPassionDescription,
            OurValuesTitle = model.OurValuesTitle,
            AboutUsOrderDescription = aboutUsDescription,
            AboutUsOrderTitle = aboutUsTitle,
            ClientOrderSteps = clientOrderSteps,
            OurValuesDescription = model.OurValuesDescription,
        };
}