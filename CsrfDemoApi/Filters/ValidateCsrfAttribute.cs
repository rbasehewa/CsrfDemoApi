using CsrfDemoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CsrfDemoApi.Filters  // Validates X-CSRF-Token against csrfToken cookie (secure mode)
{
    /// <summary>
    /// If SecureMode is ON, validate that X-CSRF-Token header equals the csrfToken cookie.
    /// For GET/HEAD, you typically skip CSRF checks (safe methods).
    /// </summary>
    public class ValidateCsrfAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cfg = context.HttpContext.RequestServices.GetRequiredService<ICsrfConfig>();
            if (!cfg.SecureMode)
            {
                base.OnActionExecuting(context);
                return;
            }

            var method = context.HttpContext.Request.Method?.ToUpperInvariant();
            if (method is "GET" or "HEAD")
            {
                base.OnActionExecuting(context);
                return; // normally no CSRF check on safe methods
            }

            var headerToken = context.HttpContext.Request.Headers["X-CSRF-Token"].FirstOrDefault();
            context.HttpContext.Request.Cookies.TryGetValue("csrfToken", out var cookieToken);

            if (string.IsNullOrWhiteSpace(headerToken) || string.IsNullOrWhiteSpace(cookieToken) || headerToken != cookieToken)
            {
                context.Result = new ObjectResult(new { message = "CSRF validation failed" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }

}