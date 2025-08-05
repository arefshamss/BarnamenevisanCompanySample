using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.Consult;

public sealed class Consult : BaseEntity<short>
{
    #region Properties

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Mobile { get; set; }
    
    public string? AdminMessage { get; set; }
    
    public int? UserId { get; set; }    

    #endregion

    #region Relations

    public User.User? User { get; set; }    

    #endregion
}