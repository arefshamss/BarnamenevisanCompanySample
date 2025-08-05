using System.Net.Sockets;
using BarnamenevisanCompany.Domain.Enums.Job;
using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Job;

public class Job : BaseEntity
{
    #region properties

    public string Title { get; set; }
    
    
    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Description { get; set; }

    public string? SalaryFrom { get; set; }
    
    public string? SalaryTo { get; set; }
    
    public string? WorkExperience { get; set; }
    
    
    public JobConditionsStatus JobConditions { get; set; }

    public DateTime ExpireDate { get; set; }

    public string Address { get; set; }
    
    public string? Latitude { get; set; }

    public string? Longitude { get; set; }
    
    public string? Skills { get; set; }
    

    #endregion
    
    #region Relations
    public ICollection<JobUserMapping> JobUserMappings { get; set; }
    
    #endregion
   
}