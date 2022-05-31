using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public class BadGatewayExceptionMiddleware : _BaseMiddleware
    {
        public BadGatewayExceptionMiddleware(RequestDelegate requestDelegate) : base(requestDelegate) { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //throw new BadGatewayException("");
                await _requestDelegate(httpContext);
            }
            catch (BadGatewayException ex)
            {
                LogExtension.CreateLog(new LogObject
                {
                    Id = Guid.NewGuid().ToString(),
                    UserLog = httpContext.User.Identity.Name,
                    InfoLog = ex.ToString(),
                    LevelLog = EnumLogLevel.Error,
                    TimeLog = DateTime.Now,
                    IPAddress = httpContext.Connection.RemoteIpAddress,
                    HttpStatusCode = HttpStatusCode.BadGateway
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;

                await httpContext.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class BadGatewayExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseBadGatewayExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BadGatewayExceptionMiddleware>();
        }
    }
}
