using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.WepApi.Middlewares;

public class ExceptiongHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BaseException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";
            var response = Response<string>.Fail(ex.Message, ex.StatusCode);
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = Response<string>.Fail(ex.Message, context.Response.StatusCode);
            await context.Response.WriteAsJsonAsync(response);
        }

    }
}