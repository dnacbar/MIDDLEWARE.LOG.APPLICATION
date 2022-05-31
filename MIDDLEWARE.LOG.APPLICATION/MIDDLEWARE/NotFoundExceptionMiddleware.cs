using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public class NotFoundExceptionMiddleware : _BaseMiddleware
    {
        public NotFoundExceptionMiddleware(RequestDelegate requestDelegate) : base(requestDelegate) { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //throw new NotFoundException("");
                await _requestDelegate(httpContext);
            }
            catch (NotFoundException ex)
            {
                LogExtension.CreateLog(new LogObject
                {
                    Id = Guid.NewGuid().ToString(),
                    UserLog = httpContext.User.Identity.Name,
                    InfoLog = ex.ToString(),
                    LevelLog = EnumLogLevel.Warning,
                    TimeLog = DateTime.Now,
                    IPAddress = httpContext.Connection.RemoteIpAddress,
                    HttpStatusCode = HttpStatusCode.NotFound
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await httpContext.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class NotFoundExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotFoundExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundExceptionMiddleware>();
        }
    }
}
