using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.Models.SiteSetting;
using BarnamenevisanCompany.Domain.Models.SmsProvider;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class ApplicationSeeds
{
    public static ModelBuilder AddApplicationSeeds(this ModelBuilder modelBuilder)
    {
        #region ContactUsInformation

        modelBuilder.Entity<ContactUsInformation>().HasData(ContactUsInformationSeeds.ContactUsInformation);

        #endregion

        #region Ticket Priorities

        modelBuilder.Entity<TicketPriority>().HasData(TicketPrioritySeeds.TicketPriorities);

        #endregion

        #region Sms Provider

        modelBuilder.Entity<SmsProvider>().HasData(SmsProviderSeeds.SmsProviders);

        #endregion

        #region AboutUs

        modelBuilder.Entity<AboutUs>().HasData(AboutUsSeeds.AboutUs);

        #endregion

        #region SiteSetting

        modelBuilder.Entity<SiteSetting>().HasData(SiteSettingSeeds.SiteSettings);

        #endregion

        #region UserSocialNetwork

        modelBuilder.Entity<UserSocialNetwork>().HasData(UserSocialNetworkSeeds.UserSocialNetworks);

        #endregion

        #region OrderType

        modelBuilder.Entity<OrderType>().HasData(OrderTypeSeeds.OrderTypes);

        #endregion

        #region Permissions

        modelBuilder.Entity<Permission>().HasData(PermissionSeeds.Permissions);

        #endregion

        #region Role

        modelBuilder.Entity<Role>().HasData(RoleSeeds.Roles);

        #endregion

        #region RolePermission
        
        foreach (var permission in PermissionSeeds.Permissions)
        {
            modelBuilder.Entity<RolePermissionMapping>().HasData([
                new(1, permission.Id) 
            ]);
        }

        #endregion

        return modelBuilder;
    }
}