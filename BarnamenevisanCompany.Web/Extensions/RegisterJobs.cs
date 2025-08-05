using BarnamenevisanCompany.Web.Jobs;
using Quartz;
using Quartz.AspNetCore;

namespace BarnamenevisanCompany.Web.Extensions;

public static class RegisterJobs
{
    public static IServiceCollection AddApplicationJobs(this IServiceCollection services)
    {
        services.AddQuartz(quartz =>
        {
            #region Close Old Tickets

            JobKey closeOldTicket = new JobKey("closeOldTicketsJobKey");    
            quartz.AddJob<CloseOldTicketsJob>(closeOldTicket);
            quartz.AddTrigger(s =>
                s.StartNow()
                    .ForJob(closeOldTicket)
                    .WithIdentity("closeOldTicketsIdentity")    
                    .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 00)));

            #endregion
            
            #region Remove Expired Chunks

            JobKey removeExpiredChunks = new JobKey("removeExpiredChunksJobKey");    
            quartz.AddJob<RemoveExpiredChunksJob>(removeExpiredChunks);
            quartz.AddTrigger(s =>
                s.StartNow()
                    .ForJob(removeExpiredChunks)
                    .WithIdentity("removeExpiredChunksIdentity")        
                    .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 00)));

            #endregion
        });


        services.AddQuartzServer(options => options.WaitForJobsToComplete = true);

        return services;
    }
}