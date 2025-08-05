using BarnamenevisanCompany.Domain.Models.Job;
using BarnamenevisanCompany.Domain.ViewModels.Admin.JobUserMapping;
using BarnamenevisanCompany.Domain.ViewModels.Client.JobUserMapping;

namespace BarnamenevisanCompany.Application.Mappers.JobMappings;

public static class JobUserMapper
{
    #region Admin

    public static AdminJobUserMappingViewModel MapToAdminJobUserMappingViewModel(this JobUserMapping model) =>
        new()
        {
            Id = model.Id,
            RequesterFullName = model.User.FirstName + " " + model.User.LastName,
            JobTitle = model.Job.Title,
            CreatedDate = model.CreatedDate,
            IsDeleted = model.IsDeleted,
            ExpireDate = model.Job.ExpireDate,
        };

    public static AdminJobUserMappingDetailsViewModel MapToAdminJobUserMappingDetailsViewModel(this JobUserMapping model) =>
        new()
        {
            Id = model.Id,
            RequesterFullName = model.User.FirstName + " " + model.User.LastName,
            RequesterMobile = model.User.Mobile,
            JobTitle = model.Job.Title,
            Attachment = model.Attachment,
            Description =  model.Description,
            CreatedDate = model.CreatedDate,
            ExpireDate = model.Job.ExpireDate,
        };

    #endregion

    #region Client

    public static JobUserMapping MapToJobUserMapping(this ClientCreateJobUserMappingViewModel model) =>
        new()
        {
            JobId = model.JobId,
            UserId = model.UserId,
            Description = model.Description,
        };

    public static ClientJobUserMappingViewModel MapToClientJobUserMappingViewModel(this JobUserMapping model) =>
        new()
        {
            Id = model.Id,
            JobTitle = model.Job.Title,
            CreatedDate = model.CreatedDate,
            UserId = model.User.Id,
        };
    
    public static ClientJobUserMappingDetailsViewModel MapToClientJobUserMappingDetailsViewModel(this JobUserMapping model) =>
        new()
        {
            Id = model.Id,
            JobTitle = model.Job.Title,
            Attachment = model.Attachment,
            Description =  model.Description,
            CreatedDate = model.CreatedDate,
            ExpireDate = model.Job.ExpireDate,
        };

    
    #endregion
}