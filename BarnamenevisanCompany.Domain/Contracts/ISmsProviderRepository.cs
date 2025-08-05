using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.SmsProvider;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface ISmsProviderRepository : IEfRepository<SmsProvider, byte>;