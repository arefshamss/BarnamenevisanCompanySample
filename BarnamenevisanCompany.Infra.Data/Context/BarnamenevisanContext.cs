using BarnamenevisanCompany.Domain.Models.AboutUs;
using BarnamenevisanCompany.Domain.Models.Blog;
using BarnamenevisanCompany.Domain.Models.ContactUs;
using BarnamenevisanCompany.Domain.Models.OurServices;
using BarnamenevisanCompany.Domain.Models.Company;
using BarnamenevisanCompany.Domain.Models.Consult;
using BarnamenevisanCompany.Domain.Models.DynamicPage;
using BarnamenevisanCompany.Domain.Models.Faq;
using BarnamenevisanCompany.Domain.Models.Honors;
using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.Models.Order;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Domain.Models.Project;
using BarnamenevisanCompany.Domain.Models.Role;
using BarnamenevisanCompany.Domain.Models.SiteSetting;
using BarnamenevisanCompany.Domain.Models.SmsProvider;
using BarnamenevisanCompany.Domain.Models.SocialNetwork;
using BarnamenevisanCompany.Domain.Models.Technology;
using BarnamenevisanCompany.Domain.Models.Ticket;
using BarnamenevisanCompany.Domain.Models.User;
using BarnamenevisanCompany.Domain.Models.UserPosition;
using BarnamenevisanCompany.Domain.Models.UserSocialNetwork;
using BarnamenevisanCompany.Domain.Models.UserSocialNetworkMapper;
using BarnamenevisanCompany.Infra.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BarnamenevisanCompany.Infra.Data.Context;

public class BarnamenevisanContext(DbContextOptions<BarnamenevisanContext> options) : DbContext(options)
{
    #region Db Sets

    #region User

    public DbSet<User> Users { get; set; }

    public DbSet<UserPosition> UserPositions { get; set; }

    #endregion

    #region Company

    public DbSet<Company> Companies { get; set; }

    public DbSet<PartnerCompany> PartnerCompanies { get; set; }

    #endregion

    #region ContactUs

    public DbSet<ContactUsInformation> ContactUsInformation { get; set; }

    public DbSet<ContactUs> ContactUs { get; set; }

    #endregion

    #region OurService

    public DbSet<OurService> OurServices { get; set; }

    #endregion

    #region Blog

    public DbSet<Blog> Blogs { get; set; }
    
    public DbSet<BlogUserMapping> BlogUserMappings { get; set; }

    #endregion

    #region Ticket

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<TicketMessage> TicketMessages { get; set; }

    public DbSet<TicketPriority> TicketPriorities { get; set; }

    #endregion

    #region Honor

    public DbSet<Honors> Honors { get; set; }

    public DbSet<HonorsGallery> HonorsGalleries { get; set; }

    #endregion

    #region DynamicPages

    public DbSet<DynamicPage> DynamicPages { get; set; }

    #endregion

    #region Faq

    public DbSet<Faq> Faqs { get; set; }

    #endregion

    #region SmsProviders

    public DbSet<SmsProvider> SmsProviders { get; set; }    

    #endregion

    #region Order

    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderType> OrderTypes { get; set; }
    
    public DbSet<OrderTypeMapping> OrderTypeMappings { get; set; }
    public DbSet<OrderStep> OrderSteps { get; set; }

    #endregion

    #region AboutUs

    public DbSet<AboutUs> AboutUs { get; set; }
    
    public DbSet<AboutUsComment> AboutUsComments { get; set; }

    #endregion

    #region Consult

    public DbSet<Consult> Consults { get; set; }

    #endregion

    #region SiteSetting

    public DbSet<SiteSetting> SiteSettings { get; set; }

    #endregion

    #region SocialNetwork

    public DbSet<SocialNetwork> SocialNetworks { get; set; }

    #endregion

    #region Project

    public DbSet<Project> Projects { get; set; }

    public DbSet<ProjectMemberMapping> ProjectMemberMappings { get; set; }

    public DbSet<ProjectGallery> ProjectGalleries { get; set; }

    public DbSet<ProjectVisitsMapping> ProjectVisitsMappings { get; set; }

    #endregion

    #region Technology

    public DbSet<Technology> Technologies { get; set; }

    public DbSet<ProjectTechnologyMapping> ProjectTechnologyMappings { get; set; }

    #endregion

    #region UserSocialNetwork

    public DbSet<UserSocialNetwork> UserSocialNetworks { get; set; }

    public DbSet<UserSocialNetworkMapping> UserSocialNetworkMappers { get; set; }
    #endregion

    #region Job

    public DbSet<Job> Jobs { get; set; }
    
    public DbSet<JobUserMapping> JobUserMappings { get; set; }

    #endregion

    #region Role

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }      

    #endregion

    #region Permission

    public DbSet<Permission> Permissions { get; set; }  
    
    public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }   

    #endregion
    
    #endregion

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .AddApplicationSeeds()
            .ApplyConfigurationsFromAssembly(GetType().Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}