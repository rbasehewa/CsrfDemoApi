using CsrfDemoApi.Utils;

namespace CsrfDemoApi.Services;

/// <summary>
/// Issues session + (optionally) CSRF token cookies.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Creates a "session" and sets cookies (sessionId + csrfToken if secure mode).
    /// Returns (username, secureMode).
    /// </summary>
    (string Username, bool SecureMode) Login(HttpContext httpContext, string username);
}