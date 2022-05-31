using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MIDDLEWARE.LOG.APPLICATION.MODEL.ENUM;
using MIDDLEWARE.LOG.APPLICATION.MODEL.EXCEPTION;
using System.Net;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public class DataBaseExceptionMiddleware : _BaseMiddleware
    {
        public DataBaseExceptionMiddleware(RequestDelegate requestDelegate) : base(requestDelegate) { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                //throw new DataBaseException("");
                await _requestDelegate(httpContext);
            }
            catch (DataBaseException ex)
            {
                LogExtension.CreateLog(new LogObject
                {
                    Id = Guid.NewGuid().ToString(),
                    UserLog = httpContext.User.Identity.Name,
                    InfoLog = ex.ToString(),
                    LevelLog = EnumLogLevel.Error,
                    TimeLog = DateTime.Now,
                    IPAddress = httpContext.Connection.RemoteIpAddress,
                    HttpStatusCode = HttpStatusCode.BadRequest 
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await httpContext.Response.WriteAsync(string.Empty);
            }
        }
    }

    public static class DataBaseExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseDataBaseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DataBaseExceptionMiddleware>();
        }
    }
}
