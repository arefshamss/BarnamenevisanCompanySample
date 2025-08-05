using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.SmsProvider;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class SmsProviderRepository(BarnamenevisanContext context) : EfRepository<SmsProvider, byte>(context), ISmsProviderRepository;   