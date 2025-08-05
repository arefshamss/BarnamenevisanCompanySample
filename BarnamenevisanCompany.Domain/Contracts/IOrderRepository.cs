using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Order;

namespace BarnamenevisanCompany.Domain.Contracts;

public interface IOrderRepository : IEfRepository<Order>;