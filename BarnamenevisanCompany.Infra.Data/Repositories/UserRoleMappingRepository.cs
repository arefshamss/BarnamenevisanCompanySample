using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.Data.Repositories.Generics;

namespace BarnamenevisanCompany.Infra.Data.Repositories;

public class UserRoleMappingRepository(BarnamenevisanContext context) : EfRepository<UserRoleMapping>(context), IUserRoleMappingRepository;