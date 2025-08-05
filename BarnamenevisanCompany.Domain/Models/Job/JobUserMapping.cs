using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Job;

public class JobUserMapping : BaseEntity
{
    #region Properties

    public string? Description { get; set; }
    
    public string Attachment { get; set; }
    
    public int JobId { get; set; }

    public int UserId { get; set; }

    #endregion

    #region Relations

    public Job Job { get; set; }

    public User.User User { get; set; }

    #endregion
}