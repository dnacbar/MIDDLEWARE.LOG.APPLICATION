using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public sealed class FatalExceptionMiddleware : _BaseMiddleware
    {
        public FatalExceptionMiddleware(RequestDelegate requestDelegate) : base(requestDelegate) { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //throw new Exception("Internal Server Error!");
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                LogExtension.CreateLog(new LogObject
                {
                    Id = Guid.NewGuid().ToString(),
                    UserLog = httpContext.User.Identity.Name,
                    InfoLog = ex.ToString(),
                    LevelLog = EnumLogLevel.Fatal,
                    TimeLog = DateTime.Now,
                    IPAddress = httpContext.Connection.RemoteIpAddress,
                    HttpStatusCode = HttpStatusCode.InternalServerError
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class FatalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseFatalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FatalExceptionMiddleware>();
        }
    }
}
