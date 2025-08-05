using BarnamenevisanCompany.Domain.ViewModels.Client.OrderStep;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.AboutUs;

public class ClientAboutUsViewModel
{
    public string TopTitle { get; set; }
    public string TopDescription { get; set; }
    public string MainDescriptionLeft { get; set; }
    public string MainDescriptionRight { get; set; }
    public string OurValuesTitle { get; set; }
    public string OurPassionDescription { get; set; }    
    public string TransparencyDescription { get; set; }
    public string OurMissionDescription { get; set; }
    public string OurValuesDescription { get; set; }
    public string AboutUsOrderTitle { get; set; }    
    public string AboutUsOrderDescription { get; set; }

    public List<ClientOrderStepViewModel> ClientOrderSteps { get; set; }
    
}