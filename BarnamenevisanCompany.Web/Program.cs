using BarnamenevisanCompany.Web.Extensions;
using Serilog;

try
{
    WebApplication.CreateBuilder(args)
        .ConfigServices()
        .ConfigPipelines();
}
catch (Exception? exception)
{
    Log.Error($"Stopped program because of exception \n exception is : {exception.Message}  \n stack trace : {exception.StackTrace}");
    throw;
}
finally
{
    await Log.CloseAndFlushAsync();
}