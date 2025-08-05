using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class UserRepository(BarnamenevisanContext context) : EfRepository<User>(context), IUserRepository;