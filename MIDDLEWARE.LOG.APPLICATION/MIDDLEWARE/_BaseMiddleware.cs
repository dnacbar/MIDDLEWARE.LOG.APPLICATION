using Microsoft.AspNetCore.Http;

namespace MIDDLEWARE.LOG.APPLICATION.LOG.MIDDLEWARE
{
    public abstract class _BaseMiddleware
    {
        protected readonly RequestDelegate _requestDelegate;

        protected _BaseMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public abstract Task InvokeAsync(HttpContext httpContext);
    }
}
