using BarnamenevisanCompany.Application.Extensions;
using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.ViewModels.Admin.Job;
using BarnamenevisanCompany.Domain.ViewModels.Client.Job;

namespace BarnamenevisanCompany.Application.Mappers.JobMappings;

public static class JobMapper
{
    #region Admin

    public static Job MapToJob(this AdminCreateJobViewModel model) =>
        new()
        {
            Title = model.Title,
            Description = model.Description,
            ShortDescription = model.ShortDescription,
            SalaryTo = model.SalaryTo,
            SalaryFrom = model.SalaryFrom,
            WorkExperience = model.WorkExperience,
            JobConditions = model.JobConditions,
            ExpireDate = model.ExpireDate.ToMiladiDateTime(),
            Address = model.Address,
            Slug = model.Slug.Trim().ToLower(),
            Skills = model.Skills,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
        };

    public static AdminJobViewModel MapToAdminJobViewModel(this Job model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            SalaryFrom = model.SalaryFrom,
            SalaryTo = model.SalaryTo,
            ExpireDate = model.ExpireDate,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted
        };

    public static AdminUpdateJobViewModel MapToAdminUpdateJobViewModel(this Job model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            SalaryFrom = model.SalaryFrom,
            SalaryTo = model.SalaryTo,
            WorkExperience = model.WorkExperience,
            JobConditions = model.JobConditions,
            ExpireDate = model.ExpireDate.ToShamsi(),
            Address = model.Address,
            Slug = model.Slug.Trim().ToLower(),
            Skills = model.Skills,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
        };

    public static void MapToJob(this Job model, AdminUpdateJobViewModel viewModel)
    {
        model.Id = viewModel.Id;
        model.Title = viewModel.Title;
        model.ShortDescription = viewModel.ShortDescription;
        model.Description = viewModel.Description;
        model.SalaryFrom = viewModel.SalaryFrom;
        model.SalaryTo = viewModel.SalaryTo;
        model.WorkExperience = viewModel.WorkExperience;
        model.JobConditions = viewModel.JobConditions;
        model.ExpireDate = viewModel.ExpireDate.ToMiladiDateTime();
        model.Address = viewModel.Address;
        model.Slug = viewModel.Slug.Trim().ToLower();
        model.Skills = viewModel.Skills;
        model.Latitude = viewModel.Latitude;
        model.Longitude = viewModel.Longitude;
    }

    #endregion

    #region Client

    public static ClientJobViewModel MapToClientJobViewModel(this Job model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            JobConditions = model.JobConditions
        };

    public static ClientJobDetailsViewModel MapToClientJobDetailsViewModel(this Job model) =>
        new()
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            SalaryFrom = model.SalaryFrom,
            SalaryTo = model.SalaryTo,
            WorkExperience = model.WorkExperience ?? "مهم نیست",
            ExpireDate = model.ExpireDate.ToShamsi(),
            Address = model.Address,
            Skills = model.Skills,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            CreatedDate = model.CreatedDate,
            JobConditions = model.JobConditions,
            Expired = model.ExpireDate < DateTime.Now,
        };

    #endregion
}