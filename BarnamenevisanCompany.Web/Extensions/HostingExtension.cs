using BarnamenevisanCompany.Application.Cache;
using BarnamenevisanCompany.Application.Statics;
using BarnamenevisanCompany.Infra.Data.Context;
using BarnamenevisanCompany.Infra.IOC;
using BarnamenevisanCompany.Web.ActionFilters;
using BarnamenevisanCompany.Web.Configurations;
using BarnamenevisanCompany.Web.Middlewares;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

namespace BarnamenevisanCompany.Web.Extensions;

public static class HostingExtension
{
    #region Services

    public static WebApplication ConfigServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllersWithViews(option => option.Filters.Add<SanitizeActionFilter>());
        
        builder.Configuration.GetSection("SiteTools").Get<SiteTools>();
        builder.Configuration.GetSection("FilePaths").Get<FilePaths>();
        builder.Services.Configure<MailSettings>(
            builder.Configuration.GetSection("MailSettings"));

        #region Max Request Size

        builder.Services.Configure<IISServerOptions>(options => { options.MaxRequestBodySize = 1000000000; });
        builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 1000000000 ; });
        builder.WebHost.ConfigureKestrel(options => { options.Limits.MaxRequestBodySize = 1000000000 ; });

        #endregion
        
        #region Data Protection

        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(builder.Configuration.GetSection("Settings:KeyDirectoryPath").Value!))
            .SetApplicationName("BarnamenevisanCompany")
            .SetDefaultKeyLifetime(
                TimeSpan.FromMinutes(
                    int.Parse(builder.Configuration.GetSection("Settings:MachineKeyLifetime").Value!)));

        #endregion

        #region Tus Config

        builder.Services.AddSingleton(TusChunkConfigurationStatic.TusChunkConfigurations);
        builder.Services.AddSingleton<ITusStore>(sp =>
        {
            var tusStorePath = Path.Combine(Directory.GetCurrentDirectory(),
                TusChunkConfigurationStatic.TusChunkConfigurations.UploadChunksPath);
            return new TusDiskStore(tusStorePath);
        });

        #endregion

        #region DbContext

        builder.Services.AddDbContext<BarnamenevisanContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("BarnamenevisanConnectionString")); });

        #endregion

        #region Config Serilog

        builder.Logging.ClearProviders().AddSerilog();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(s => s.Level == LogEventLevel.Warning)
                .WriteTo.File(
                    path: "Logs/log-warning-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            )
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(s => s.Level == LogEventLevel.Error)
                .WriteTo.File(
                    path: "Logs/log-error-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            )
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(s => s.Level == LogEventLevel.Fatal)
                .WriteTo.File(
                    path: "Logs/log-fatal-.txt",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            ).CreateLogger();

        Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

        builder.Host.UseSerilog();

        #endregion

        #region Register Services

        builder.Services
            .AddCookiePolicy(options => { options.Secure = CookieSecurePolicy.Always; })
            .AddSession()
            .RegisterServices();

        #endregion

        #region GoogleReCaptcha

        builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

        #endregion

        #region Easy Cashing

        builder.Services.AddEasyCaching(options => options.UseInMemory(CacheProviders.InMemoryCachingProviderName))
            .AddApplicationAuthentication()
            .AddApplicationJobs();

        #endregion

        return builder.Build();
    }

    #endregion

    #region Pipelines

    public static void ConfigPipelines(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
        }

        app.UseSession();

        app.UseHttpsRedirection();
        app.UseRouting();

        // app.MapStaticAssets();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseTusChunkUpload(cfg =>
        {
            cfg.ExpirationType = TusChunkConfigurationStatic.TusChunkConfigurations.ExpirationType;
            cfg.ChunksExpirationDuration = TusChunkConfigurationStatic.TusChunkConfigurations.ChunksExpirationDuration;
            cfg.EndpointPath = TusChunkConfigurationStatic.TusChunkConfigurations.EndpointPath;
            cfg.DefaultUploadPath = TusChunkConfigurationStatic.TusChunkConfigurations.DefaultUploadPath;
            cfg.UploadChunksPath = TusChunkConfigurationStatic.TusChunkConfigurations.UploadChunksPath;
            cfg.MaxAllowedUploadSizeInMegaByte =
                TusChunkConfigurationStatic.TusChunkConfigurations.MaxAllowedUploadSizeInMegaByte;
        });

        app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }

    #endregion
}