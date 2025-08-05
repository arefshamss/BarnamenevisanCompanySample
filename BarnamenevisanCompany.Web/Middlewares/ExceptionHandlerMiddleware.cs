namespace BarnamenevisanCompany.Web.Middlewares;

public class ExceptionHandlerMiddleware(
    ILogger<ExceptionHandlerMiddleware> logger,
    RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);

            if (!context.Response.HasStarted)
            {
                switch (context.Response.StatusCode)
                {
                    case 500:
                        context.Response.Redirect("/server-error");
                        break;
                    
                    case 404:
                        context.Response.Redirect("/not-found");
                        break;
                    
                    case 403:
                        context.Response.Redirect("/not-found");
                        break;
                    
                    case 405:
                        context.Response.Redirect("/not-found");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(ex, $"Error ID: {errorId}, Message: {ex.Message}");

            context.Response.Redirect("/server-error");
        }
    }
}