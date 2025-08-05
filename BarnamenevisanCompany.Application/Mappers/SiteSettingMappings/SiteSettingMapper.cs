using BarnamenevisanCompany.Domain.Models.SiteSetting;
using BarnamenevisanCompany.Domain.ViewModels.Admin.SiteSetting;

namespace BarnamenevisanCompany.Application.Mappers.SiteSettingMappings;

public static class SiteSettingMapper
{
    public static AdminSiteSettingUpdateViewModel MapAdminSiteSettingUpdateViewModel(this SiteSetting model) =>
        new()
        {
            ConsultInformation = model.ConsultInformationText,
            AboutUs = model.AboutUsFooter,
            ContactUsHeaderText = model.ContactUsHeaderText,
            ContactUs = model.ContactUsFooter,
            IndexParagraph = model.IndexParagraph,
            IndexTitle = model.IndexTitle,
            IndexOrderParagraph = model.IndexOrderParaghraph,
            IndexOrderTitle = model.IndexOrderTitle,
            CompletedProject = model.CompletedProject,
            SiteLogoImageName = model.SiteLogo,
            SiteFavIconImageName = model.FavIcon,
            JobListDescription = model.JobListDescription,
            JobListTitle = model.JobListTitle,
            JobListImageName = model.JobListImageName,
            OurServiceDescription = model.OurServiceDescription,
            OurServicePageTitle = model.OurServicePageTitle,
            ConsultSmsMessage = model.ConsultSmsMessage,
            NotificationPhoneNumber = model.NotificationPhoneNumber,
        };


    public static void MapAdminSiteSetting(this SiteSetting siteSetting, AdminSiteSettingUpdateViewModel model)
    {
        siteSetting.ConsultInformationText = model.ConsultInformation;
        siteSetting.AboutUsFooter = model.AboutUs;
        siteSetting.ContactUsHeaderText = model.ContactUsHeaderText;
        siteSetting.ContactUsFooter = model.ContactUs;
        siteSetting.IndexParagraph = model.IndexParagraph;
        siteSetting.IndexTitle = model.IndexTitle;
        siteSetting.IndexOrderParaghraph = model.IndexOrderParagraph;
        siteSetting.IndexOrderTitle = model.IndexOrderTitle;
        siteSetting.CompletedProject = model.CompletedProject;
        siteSetting.SiteLogo = model.SiteLogoImageName;
        siteSetting.FavIcon = model.SiteFavIconImageName;
        siteSetting.JobListImageName = model.JobListImageName;
        siteSetting.JobListTitle = model.JobListTitle;
        siteSetting.JobListDescription = model.JobListDescription;
        siteSetting.OurServiceDescription = model.OurServiceDescription;
        siteSetting.OurServicePageTitle = model.OurServicePageTitle;
        siteSetting.ConsultSmsMessage = model.ConsultSmsMessage;
        siteSetting.NotificationPhoneNumber = model.NotificationPhoneNumber;
    }
}