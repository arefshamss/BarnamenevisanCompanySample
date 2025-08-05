using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class OrderTypeMappingRepository (BarnamenevisanContext context) : EfRepository<OrderTypeMapping>(context), IOrderTypeMappingRepository;