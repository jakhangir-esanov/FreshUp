using FreshUp.Domain.Exceptions;

namespace FreshUp.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate requestDelegate;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;
    public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.requestDelegate = requestDelegate;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await this.requestDelegate.Invoke(httpContext);
        }

        catch (AlreadyExistException ex)
        {
            httpContext.Response.StatusCode = 403;
            await httpContext.Response.WriteAsync(ex.Message);
        }

        catch (NotFoundException ex)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsJsonAsync(ex.Message);
        }

        catch (CustomException ex)
        {
            await httpContext.Response.WriteAsJsonAsync(ex.Message);
        }

        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            await httpContext.Response.WriteAsJsonAsync(ex.Message);
        }
    }
}
