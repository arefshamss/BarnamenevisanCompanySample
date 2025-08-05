using BarnamenevisanCompany.Domain.Models.Permission;
using BarnamenevisanCompany.Infra.Data.Statics;

namespace BarnamenevisanCompany.Infra.Data.Seeds;

public static class PermissionSeeds
{
    public static List<Permission> Permissions { get; } =
    [
        #region Admin Dashboard

        new()
        {
            Id = 1,
            DisplayName = "پنل مدیریت",
            UniqueName = PermissionsName.Admin,
            CreatedDate = SeedStaticDateTime.Date
        },

        #endregion

        #region User

        new()
        {
            Id = 2,
            DisplayName = "مدیریت کاربران",
            UniqueName = PermissionsName.UserManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 3,
            DisplayName = "لیست کاربران",
            UniqueName = PermissionsName.UserList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 2
        },
        new()
        {
            Id = 4,
            DisplayName = "جزئیات کاربر",
            UniqueName = PermissionsName.UserDetails,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 2
        },
        new()
        {
            Id = 5,
            DisplayName = "افزودن کاربر",
            UniqueName = PermissionsName.CreateUser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 2
        },
        new()
        {
            Id = 6,
            DisplayName = "ویرایش کاربر",
            UniqueName = PermissionsName.UpdateUser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 2
        },
        new()
        {
            Id = 7,
            DisplayName = "حذف یا بازگردانی کاربر",
            UniqueName = PermissionsName.DeleteOrRecoverUser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 2
        },

        #endregion

        #region Ticket

        new()
        {
            Id = 8,
            DisplayName = "مدیریت تیکت ها",
            UniqueName = PermissionsName.TicketManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 9,
            DisplayName = "لیست تیکت ها",
            UniqueName = PermissionsName.TicketList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },
        new()
        {
            Id = 10,
            DisplayName = "جزئیات تیکت",
            UniqueName = PermissionsName.TicketDetails,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },
        new()
        {
            Id = 11,
            DisplayName = "افزودن تیکت",
            UniqueName = PermissionsName.CreateTicket,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },
        new()
        {
            Id = 12,
            DisplayName = "ویرایش تیکت",
            UniqueName = PermissionsName.UpdateTicket,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },
        new()
        {
            Id = 13,
            DisplayName = "حذف یا بازگردانی تیکت",
            UniqueName = PermissionsName.DeleteOrRecoverUser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },
        new()
        {
            Id = 14,
            DisplayName = "تغییر وضعیت تیکت",
            UniqueName = PermissionsName.ToggleCloseTicket,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 8
        },

        #endregion

        #region TicketMessages

        new()
        {
            Id = 15,
            DisplayName = "مدیریت پیام تیکت",
            UniqueName = PermissionsName.TicketMessageManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 16,
            DisplayName = "افزودن پیام تیکت",
            UniqueName = PermissionsName.CreateTicketMessage,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 15
        },

        #endregion

        #region Blog

        new()
        {
            Id = 17,
            DisplayName = "مدیریت بلاگ ها",
            UniqueName = PermissionsName.BlogManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 18,
            DisplayName = "لیست بلاگ ها",
            UniqueName = PermissionsName.BlogList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 17
        },
        new()
        {
            Id = 19,
            DisplayName = "افزودن بلاگ",
            UniqueName = PermissionsName.CreateBlog,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 17
        },
        new()
        {
            Id = 20,
            DisplayName = "ویرایش بلاگ",
            UniqueName = PermissionsName.UpdateBlog,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 17
        },
        new()
        {
            Id = 21,
            DisplayName = "حذف یا بازگردانی بلاگ",
            UniqueName = PermissionsName.DeleteOrRecoverBlog,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 17
        },

        #endregion

        #region Company

        new()
        {
            Id = 22,
            DisplayName = "مدیریت شرکت ها",
            UniqueName = PermissionsName.CompanyManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 23,
            DisplayName = "لیست شرکت ها",
            UniqueName = PermissionsName.CompanyList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 22
        },
        new()
        {
            Id = 24,
            DisplayName = "افزودن شرکت",
            UniqueName = PermissionsName.CreateCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 22
        },
        new()
        {
            Id = 25,
            DisplayName = "ویرایش شرکت",
            UniqueName = PermissionsName.UpdateCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 22
        },
        new()
        {
            Id = 26,
            DisplayName = "حذف یا بازگردانی شرکت",
            UniqueName = PermissionsName.DeleteOrRecoverCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 22
        },

        #endregion

        #region Consult

        new()
        {
            Id = 27,
            DisplayName = "مدیریت مشاوره ها",
            UniqueName = PermissionsName.ConsultManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 28,
            DisplayName = "لیست مشاوره ها",
            UniqueName = PermissionsName.ConsultList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 27
        },
        new()
        {
            Id = 29,
            DisplayName = "جزئیات مشاوره",
            UniqueName = PermissionsName.ConsultDetails,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 27
        },
        new()
        {
            Id = 30,
            DisplayName = "پاسخ به مشاوره",
            UniqueName = PermissionsName.AnswerConsult,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 27
        },
        new()
        {
            Id = 31,
            DisplayName = "حذف یا بازگردانی مشاوره",
            UniqueName = PermissionsName.DeleteOrRecoverConsult,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 27
        },

        #endregion

        #region DynamicPage

        new()
        {
            Id = 32,
            DisplayName = "مدیریت صفحات داینامیک",
            UniqueName = PermissionsName.DynamicPageManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 33,
            DisplayName = "لیست صفحات داینامیک",
            UniqueName = PermissionsName.DynamicPageList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 32
        },
        new()
        {
            Id = 34,
            DisplayName = "افزودن صفحه داینامیک",
            UniqueName = PermissionsName.CreateDynamicPage,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 32
        },
        new()
        {
            Id = 35,
            DisplayName = "ویرایش صفحه داینامیک",
            UniqueName = PermissionsName.UpdateDynamicPage,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 32
        },
        new()
        {
            Id = 36,
            DisplayName = "حذف یا بازگردانی صفحه داینامیک",
            UniqueName = PermissionsName.DeleteOrRecoverDynamicPage,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 32
        },

        #endregion

        #region Faq

        new()
        {
            Id = 37,
            DisplayName = "مدیریت سوالات متداول",
            UniqueName = PermissionsName.FaqManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 38,
            DisplayName = "لیست سوالات متداول",
            UniqueName = PermissionsName.FaqList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 37
        },
        new()
        {
            Id = 39,
            DisplayName = "افزودن سوالات متداول",
            UniqueName = PermissionsName.CreateFaq,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 37
        },
        new()
        {
            Id = 40,
            DisplayName = "ویرایش سوالات متداول",
            UniqueName = PermissionsName.UpdateFaq,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 37
        },
        new()
        {
            Id = 41,
            DisplayName = "حذف یا بازگردانی سوالات متداول",
            UniqueName = PermissionsName.DeleteOrRecoverFaq,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 37
        },

        #endregion

        #region Honor

        new()
        {
            Id = 42,
            DisplayName = "مدیریت افتخارات",
            UniqueName = PermissionsName.HonorManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 43,
            DisplayName = "لیست افتخارات",
            UniqueName = PermissionsName.HonorList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 42
        },
        new()
        {
            Id = 44,
            DisplayName = "افزودن افتخار",
            UniqueName = PermissionsName.CreateHonor,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 42
        },
        new()
        {
            Id = 45,
            DisplayName = "ویرایش افتخار",
            UniqueName = PermissionsName.UpdateHonor,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 42
        },
        new()
        {
            Id = 46,
            DisplayName = "حذف یا بازگردانی افتخار",
            UniqueName = PermissionsName.DeleteOrRecoverHonor,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 42
        },
        new()
        {
            Id = 47,
            DisplayName = "حذف تیزر افتخار",
            UniqueName = PermissionsName.DeleteHonorTeaser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 42
        },

        #endregion

        #region HonorGallery

        new()
        {
            Id = 48,
            DisplayName = "مدیریت گالری افتخار",
            UniqueName = PermissionsName.HonorGalleryManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 49,
            DisplayName = "تصاویر گالری افتخار",
            UniqueName = PermissionsName.HonorGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 48
        },
        new()
        {
            Id = 50,
            DisplayName = "افزودن عکس به گالری افتخار",
            UniqueName = PermissionsName.AddImageToHonorGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 48
        },
        new()
        {
            Id = 51,
            DisplayName = "حذف عکس از گالری افتخار",
            UniqueName = PermissionsName.DeleteImageFromHonorGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 48
        },

        #endregion

        #region Job

        new()
        {
            Id = 52,
            DisplayName = "مدیریت موقعیت های شفلی",
            UniqueName = PermissionsName.JobManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 53,
            DisplayName = "لیست موقعیت های شفلی",
            UniqueName = PermissionsName.JobList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 52
        },
        new()
        {
            Id = 54,
            DisplayName = "افزودن موقعیت شفلی",
            UniqueName = PermissionsName.CreateJob,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 52
        },
        new()
        {
            Id = 55,
            DisplayName = "ویرایش موقعیت شفلی",
            UniqueName = PermissionsName.UpdateJob,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 52
        },
        new()
        {
            Id = 56,
            DisplayName = "حذف یا بازگردانی موقعیت شفلی",
            UniqueName = PermissionsName.DeleteOrRecoverJob,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 52
        },

        #endregion

        #region JobUserMapping

        new()
        {
            Id = 57,
            DisplayName = "مدیریت درخواست موقعیت های شفلی",
            UniqueName = PermissionsName.JobUserMappingManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 58,
            DisplayName = "لیست درخواست موقعیت های شفلی",
            UniqueName = PermissionsName.JobUserMappingList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 57
        },
        new()
        {
            Id = 59,
            DisplayName = "جزئیات درخواست موقعیت شفلی",
            UniqueName = PermissionsName.JobUserMappingDetails,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 57
        },
        new()
        {
            Id = 60,
            DisplayName = "حذف یا بازگردانی درخواست موقعیت شفلی",
            UniqueName = PermissionsName.DeleteOrRecoverJobUserMapping,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 57
        },

        #endregion

        #region Order

        new()
        {
            Id = 61,
            DisplayName = "مدیریت سفارشات",
            UniqueName = PermissionsName.OrderManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 62,
            DisplayName = "لیست سفارشات",
            UniqueName = PermissionsName.OrderList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 61
        },
        new()
        {
            Id = 63,
            DisplayName = "تغییر وضعیت سفارش",
            UniqueName = PermissionsName.ChangeOrderStatus,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 61
        },
        new()
        {
            Id = 64,
            DisplayName = "پاسخ به سفارش",
            UniqueName = PermissionsName.AnswerToOrder,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 61
        },
        new()
        {
            Id = 65,
            DisplayName = "حذف یا بازگردانی سفارش",
            UniqueName = PermissionsName.DeleteOrRecoverOrder,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 61
        },

        #endregion

        #region OrderType

        new()
        {
            Id = 66,
            DisplayName = "مدیریت انواع سفارش",
            UniqueName = PermissionsName.OrderTypeManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 67,
            DisplayName = "لیست انواع سفارش",
            UniqueName = PermissionsName.OrderTypeList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 66
        },
        new()
        {
            Id = 68,
            DisplayName = "افزودن نوع سفارش",
            UniqueName = PermissionsName.CreateOrderType,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 66
        },
        new()
        {
            Id = 69,
            DisplayName = "ویرایش نوع سفارش",
            UniqueName = PermissionsName.UpdateOrderType,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 66
        },
        new()
        {
            Id = 70,
            DisplayName = "حذف یا بازگردانی نوع سفارش",
            UniqueName = PermissionsName.DeleteOrRecoverOrderType,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 66
        },

        #endregion

        #region OurService

        new()
        {
            Id = 71,
            DisplayName = "مدیریت خدمات",
            UniqueName = PermissionsName.OurServiceManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 72,
            DisplayName = "لیست خدمات",
            UniqueName = PermissionsName.OurServiceList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 71
        },
        new()
        {
            Id = 73,
            DisplayName = "افزودن خدمات",
            UniqueName = PermissionsName.CreateOurService,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 71
        },
        new()
        {
            Id = 74,
            DisplayName = "ویرایش خدمات",
            UniqueName = PermissionsName.UpdateOurService,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 71
        },
        new()
        {
            Id = 75,
            DisplayName = "حذف یا بازگردانی نوع خدمات",
            UniqueName = PermissionsName.DeleteOrRecoverOurService,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 71
        },

        #endregion

        #region PartnerCompany

        new()
        {
            Id = 76,
            DisplayName = "مدیریت سایت های همکار",
            UniqueName = PermissionsName.PartnerCompanyManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 77,
            DisplayName = "لیست سایت های همکار",
            UniqueName = PermissionsName.PartnerCompanyList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 76
        },
        new()
        {
            Id = 78,
            DisplayName = "افزودن سایت همکار",
            UniqueName = PermissionsName.CreatePartnerCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 76
        },
        new()
        {
            Id = 79,
            DisplayName = "ویرایش سایت همکار",
            UniqueName = PermissionsName.UpdatePartnerCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 76
        },
        new()
        {
            Id = 80,
            DisplayName = "حذف یا بازگردانی سایت همکار",
            UniqueName = PermissionsName.DeleteOrRecoverPartnerCompany,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 76
        },

        #endregion

        #region Project

        new()
        {
            Id = 81,
            DisplayName = "مدیریت پروژه ها",
            UniqueName = PermissionsName.ProjectManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 82,
            DisplayName = "لیست پروژه ها",
            UniqueName = PermissionsName.ProjectList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 81
        },
        new()
        {
            Id = 83,
            DisplayName = "افزودن پروژه",
            UniqueName = PermissionsName.CreateProject,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 81
        },
        new()
        {
            Id = 84,
            DisplayName = "ویرایش پروژه",
            UniqueName = PermissionsName.UpdateProject,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 81
        },
        new()
        {
            Id = 85,
            DisplayName = "حذف یا بازگردانی پروژه",
            UniqueName = PermissionsName.DeleteOrRecoverProject,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 81
        },
        new()
        {
            Id = 86,
            DisplayName = "حذف تیزر پروژه",
            UniqueName = PermissionsName.DeleteProjectTeaser,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 81
        },

        #endregion

        #region ProjectGallery

        new()
        {
            Id = 87,
            DisplayName = "مدیریت گالری پروژه",
            UniqueName = PermissionsName.ProjectGalleryManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 88,
            DisplayName = "تصاویر گالری پروژه",
            UniqueName = PermissionsName.ProjectGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 87
        },
        new()
        {
            Id = 89,
            DisplayName = "افزودن عکس به گالری پروژه",
            UniqueName = PermissionsName.AddImageToProjectGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 87
        },
        new()
        {
            Id = 90,
            DisplayName = "حذف عکس از گالری پروژه",
            UniqueName = PermissionsName.DeleteImageFromProjectGallery,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 87
        },

        #endregion

        //Available 91 - 95

        #region SocialNetwork

        new()
        {
            Id = 96,
            DisplayName = "مدیریت شبکه های اجتماعی",
            UniqueName = PermissionsName.SocialNetworkManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 97,
            DisplayName = "لیست شبکه های اجتماعی",
            UniqueName = PermissionsName.SocialNetworkList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 96
        },
        new()
        {
            Id = 98,
            DisplayName = "افزودن شبکه ی اجتماعی",
            UniqueName = PermissionsName.CreateSocialNetwork,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 96
        },
        new()
        {
            Id = 99,
            DisplayName = "ویرایش شبکه ی اجتماعی",
            UniqueName = PermissionsName.UpdateSocialNetwork,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 96
        },
        new()
        {
            Id = 100,
            DisplayName = "حذف یا بازگردانی شبکه ی اجتماعی",
            UniqueName = PermissionsName.DeleteOrRecoverSocialNetwork,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 96
        },

        #endregion

        #region Technology

        new()
        {
            Id = 101,
            DisplayName = "مدیریت تکنولوژی ها",
            UniqueName = PermissionsName.TechnologyManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 102,
            DisplayName = "لیست تکنولوژی ها",
            UniqueName = PermissionsName.TechnologyList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 101
        },
        new()
        {
            Id = 103,
            DisplayName = "افزودن تکنولوژی",
            UniqueName = PermissionsName.CreateTechnology,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 101
        },
        new()
        {
            Id = 104,
            DisplayName = "ویرایش تکنولوژی",
            UniqueName = PermissionsName.UpdateTechnology,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 101
        },
        new()
        {
            Id = 105,
            DisplayName = "حذف یا بازگردانی تکنولوژی",
            UniqueName = PermissionsName.DeleteOrRecoverTechnology,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 101
        },

        #endregion

        #region TicketPriority

        new()
        {
            Id = 106,
            DisplayName = "مدیریت اولویت تیکت",
            UniqueName = PermissionsName.TicketPriorityManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 107,
            DisplayName = "لیست اولویت تیکت",
            UniqueName = PermissionsName.TicketPriorityList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 106
        },
        new()
        {
            Id = 108,
            DisplayName = "افزودن اولویت تیکت",
            UniqueName = PermissionsName.CreateTicketPriority,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 106
        },
        new()
        {
            Id = 109,
            DisplayName = "ویرایش اولویت تیکت",
            UniqueName = PermissionsName.UpdateTicketPriority,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 106
        },
        new()
        {
            Id = 110,
            DisplayName = "حذف یا بازگردانی اولویت تیکت",
            UniqueName = PermissionsName.DeleteOrRecoverTicketPriority,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 106
        },

        #endregion

        #region UserPosition

        new()
        {
            Id = 111,
            DisplayName = "مدیریت تیم برنامه نویسان",
            UniqueName = PermissionsName.UserPositionManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 112,
            DisplayName = "لیست اعضای تیم برنامه نویسان",
            UniqueName = PermissionsName.UserPositionList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 111
        },
        new()
        {
            Id = 113,
            DisplayName = "افزودن به تیم برنامه نویسان",
            UniqueName = PermissionsName.CreateUserPosition,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 111
        },
        new()
        {
            Id = 114,
            DisplayName = "ویرایش تیم برنامه نویسان",
            UniqueName = PermissionsName.UpdateUserPosition,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 111
        },
        new()
        {
            Id = 115,
            DisplayName = "حذف یا بازگردانی از تیم برنامه نویسان",
            UniqueName = PermissionsName.DeleteOrRecoverUserPosition,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 111
        },

        #endregion

        #region ContactUs

        new()
        {
            Id = 116,
            DisplayName = "مدیریت تماس با ما",
            UniqueName = PermissionsName.ContactUsManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 117,
            DisplayName = "لیست تماس با ما",
            UniqueName = PermissionsName.ContactUsList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 116
        },
        new()
        {
            Id = 118,
            DisplayName = "ویرایش اطلاعات تماس با ما",
            UniqueName = PermissionsName.UpdateContactUsInformation,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 116
        },
        new()
        {
            Id = 119,
            DisplayName = "پاسخ به درخواست تماس با ما",
            UniqueName = PermissionsName.AnswerContactUs,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 116
        },
        new()
        {
            Id = 120,
            DisplayName = "حذف یا بازگردانی تماس با ما",
            UniqueName = PermissionsName.DeleteOrRecoverContactUs,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 116
        },

        #endregion

        #region AboutUs

        new()
        {
            Id = 121,
            DisplayName = "ویرایش درباره ی ما",
            UniqueName = PermissionsName.UpdateAboutUs,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },

        #endregion

        #region SiteSettings

        new()
        {
            Id = 122,
            DisplayName = "ویرایش تنظیمات سایت",
            UniqueName = PermissionsName.UpdateSiteSettings,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },

        #endregion

        #region Role

        new()
        {
            Id = 123,
            DisplayName = "مدیریت نقش ها",
            UniqueName = PermissionsName.RoleManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 124,
            DisplayName = "لیست نقش ها",
            UniqueName = PermissionsName.RoleList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },
        new()
        {
            Id = 125,
            DisplayName = "افزودن نقش",
            UniqueName = PermissionsName.CreateRole,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },
        new()
        {
            Id = 126,
            DisplayName = "ویرایش نقش",
            UniqueName = PermissionsName.UpdateRole,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },
        new()
        {
            Id = 127,
            DisplayName = "حذف یا بازگردانی نقش",
            UniqueName = PermissionsName.DeleteOrRecoverRole,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },
        new()
        {
            Id = 128,
            DisplayName = "افزودن دسترسی به نقش",
            UniqueName = PermissionsName.SetPermissionToRole,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },
        new()
        {
            Id = 129,
            DisplayName = "افزودن نقش به کاربر",
            UniqueName = PermissionsName.SetUserToRole,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 123
        },

        #endregion

        #region OrderStep

        new()
        {
            Id = 130,
            DisplayName = "مدیریت مراحل سفارش",
            UniqueName = PermissionsName.OrderStepManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 131,
            DisplayName = "لیست مراحل سفارش",
            UniqueName = PermissionsName.OrderStepList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 130
        },
        new()
        {
            Id = 132,
            DisplayName = "افزودن مرحله سفارش",
            UniqueName = PermissionsName.CreateOrderStep,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 130
        },
        new()
        {
            Id = 133,
            DisplayName = "ویرایش مرحله صفارش",
            UniqueName = PermissionsName.UpdateOrderStep,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 130
        },
        new()
        {
            Id = 134,
            DisplayName = "حذف یا بازگردانی مرحله سفارش",
            UniqueName = PermissionsName.DeleteOrRecoverOrderStep,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 130
        },

        #endregion

        #region AboutUsComment

        new()
        {
            Id = 135,
            DisplayName = "مدیریت کامنت ها ",
            UniqueName = PermissionsName.AboutUsCommentManagement,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 1
        },
        new()
        {
            Id = 136,
            DisplayName = "لیست کامنت ها ",
            UniqueName = PermissionsName.AboutUsCommentList,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 135
        },
        new()
        {
            Id = 137,
            DisplayName = "افزودن کامنت ",
            UniqueName = PermissionsName.CreateAboutUsComment,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 135
        },
        new()
        {
            Id = 138,
            DisplayName = "ویرایش کامنت ",
            UniqueName = PermissionsName.UpdateAboutUsComment,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 135
        },
        new()
        {
            Id = 139,
            DisplayName = "حذف یا بازگردانی کامنت ",
            UniqueName = PermissionsName.DeleteOrRecoverAboutUsComment,
            CreatedDate = SeedStaticDateTime.Date,
            ParentId = 135
        },

        #endregion

        //Available ID : 140 ...
    ];
}