using Microsoft.AspNetCore.Mvc;
using CsrfDemoApi.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CsrfDemoApi.Filters; // Ensures a valid session cookie exists
/// <summary>
/// Ensures that a valid "sessionId" cookie exists and maps to a user.
/// Adds the username to HttpContext.Items["username"] for use in controllers.
/// </summary>
public class SessionRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var store = context.HttpContext.RequestServices.GetRequiredService<ISessionStore>();

        if (!context.HttpContext.Request.Cookies.TryGetValue("sessionId", out var sessionId) ||
            !store.TryGet(sessionId, out var username))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Make username available to action
        context.HttpContext.Items["username"] = username;
        base.OnActionExecuting(context);
    }
}
