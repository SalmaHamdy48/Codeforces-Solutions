namespace UniversitySystem.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something went wrong!");
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            
            await httpContext.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                message = ex.Message,
                innerException = ex.InnerException?.Message
            }));
        }
    }
}