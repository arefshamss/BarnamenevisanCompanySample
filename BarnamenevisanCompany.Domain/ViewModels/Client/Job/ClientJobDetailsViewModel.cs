using BarnamenevisanCompany.Domain.Enums.Job;
using Newtonsoft.Json;

namespace BarnamenevisanCompany.Domain.ViewModels.Client.Job;

public class ClientJobDetailsViewModel
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string? SalaryFrom { get; set; }
    
    public string? SalaryTo { get; set; }
    
    public string WorkExperience { get; set; }
    
    public string ExpireDate { get; set; } 
    
    public bool Expired { get; set; }
    
    public string Address { get; set; }
    
    public string? Skills { get; set; }   
    
    public string? Latitude { get; set; }

    public string? Longitude { get; set; }
    
    public JobConditionsStatus JobConditions { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public List<string> SkillList
    {
        get
        {
            try
            {
                var items = JsonConvert.DeserializeObject<List<SkillItem>>(Skills);
                return items?.Select(x => x.Value).ToList() ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}

public class SkillItem
{
    public string Value { get; set; }
}
