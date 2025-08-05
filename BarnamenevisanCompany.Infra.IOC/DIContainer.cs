using BarnamenevisanCompany.Application.Services.Implementations;
using BarnamenevisanCompany.Application.Services.Interfaces;
using BarnamenevisanCompany.Domain.Attributes;
using BarnamenevisanCompany.Domain.Contracts;
using BarnamenevisanCompany.Domain.Contracts.Generics;
using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BarnamenevisanCompany.Infra.IOC
{
    public static class DIContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactUsInformationService, ContactUsInformationService>();
            services.AddScoped<IOurServiceService, OurServiceService>();
            services.AddScoped<IUserPositionService, UserPositionService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IPartnerCompanyService, PartnerCompanyService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketPriorityService, TicketPriorityService>();
            services.AddScoped<IHonorsService, HonorsService>();
            services.AddScoped<IHonorsGalleryService, HonorsGalleryService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IDynamicPageService, DynamicPageService>();
            services.AddScoped<IBlogUserMappingService, BlogUserMappingService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderTypeService, OrderTypeService>();
            services.AddScoped<IConsultService, ConsultService>();
            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<ISocialNetworkService, SocialNetworkService>();
            services.AddScoped<ITechnologyService, TechnologyService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectGalleryService, ProjectGalleryService>();
            services.AddScoped<IProjectVisitsMappingService, ProjectVisitsMappingService>();
            services.AddScoped<IUserSocialNetworkService, UserSocialNetworkService>();
            services.AddScoped<IUserSocialNetworkMapperService, UserSocialNetworkMapperService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobUserMappingService, JobUserMappingService>();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IOrderStepService, OrderStepService>();
            services.AddScoped<IAboutUsCommentService, AboutUsCommentService>();

            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactUsInformationRepository, ContactUsInformationRepository>();
            services.AddScoped<IOurServiceRepository, OurServiceRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPartnerCompanyRepository, PartnerCompanyRepository>();
            services.AddScoped<IUserPositionRepository, UserPositionRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<ITicketPriorityRepository, TicketPriorityRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IHonorsRepository, HonorsRepository>();
            services.AddScoped<IHonorsGalleryRepository, HonorsGalleryRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<IDynamicPageRepository, DynamicPageRepository>();
            services.AddScoped<IBlogUserMappingRepository, BlogUserMappingRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<ISmsProviderRepository, SmsProviderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IOrderTypeMappingRepository, OrderTypeMappingRepository>();
            services.AddScoped<IAboutUsRepository, AboutUsRepository>();
            services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
            services.AddScoped<IConsultRepository, ConsultRepository>();
            services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>();
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTechnologyMappingRepository, ProjectTechnologyMappingRepository>();
            services.AddScoped<IProjectMemberMappingRepository, ProjectMemberMappingRepository>();
            services.AddScoped<IProjectGalleryRepository, ProjectGalleryRepository>();
            services.AddScoped<IProjectVisitsMappingRepository, ProjectVisitsMappingRepository>();
            services.AddScoped<IUserSocialNetworkMappingRepository, UserSocialNetworkMappingRepository>();
            services.AddScoped<IUserSocialNetworkRepository, UserSocialNetworkRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IJobUserMappingRepository, JobUserMappingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleMappingRepository, UserRoleMappingRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IOrderStepRepository, OrderStepRepository>();
            services.AddScoped<IAboutUsCommentRepository, AboutUsCommentRepository>();

            #endregion

            return services;
        }
    }
}