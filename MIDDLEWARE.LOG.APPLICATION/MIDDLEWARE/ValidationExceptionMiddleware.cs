using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public class BadRequestExceptionMiddleware : _BaseMiddleware
    {
        public BadRequestExceptionMiddleware(RequestDelegate requestDelegate) : base(requestDelegate) { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //throw new BadRequestException("");
                await _requestDelegate(httpContext);
            }
            catch (BadRequestException ex)
            {
                LogExtension.CreateLog(new LogObject
                {
                    Id = Guid.NewGuid().ToString(),
                    UserLog = httpContext.User.Identity.Name,
                    InfoLog = ex.ToString(),
                    LevelLog = EnumLogLevel.Warning,
                    TimeLog = DateTime.Now,
                    IPAddress = httpContext.Connection.RemoteIpAddress,
                    HttpStatusCode = HttpStatusCode.BadRequest
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class ValidationExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BadRequestExceptionMiddleware>();
        }
    }
}
