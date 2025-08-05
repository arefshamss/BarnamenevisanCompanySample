using BarnamenevisanCompany.Domain.Enums.SmsProvider;
using BarnamenevisanCompany.Domain.Models.Common;

namespace BarnamenevisanCompany.Domain.Models.SmsProvider;

public sealed class SmsProvider : BaseEntity<byte>
{
    #region Properties

    public SmsProviderType Type { get; set; }   
    public required string ApiKey { get; set; }  
    public bool IsDefault { get; set; }
    
    #endregion
}