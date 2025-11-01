using CsrfDemoApi.Utils;

namespace CsrfDemoApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISessionStore _store;
        private readonly ICsrfConfig _config;

        public AuthService(ISessionStore store, ICsrfConfig config)
        {
            _store = store;
            _config = config;
        }

        public (string Username, bool SecureMode) Login(HttpContext httpContext, string username)
        {
            // Generate a pseudo session id for demo
            var sessionId = Guid.NewGuid().ToString("N");

            // Save "session"
            _store.Set(sessionId, username);

            // HttpOnly cookie for the session
            CookieHelper.SetCookie(httpContext.Response, "sessionId", sessionId, httpOnly: true);

            if (_config.SecureMode)
            {
                // Double-submit token pattern:
                //  - a cookie readable by client JS (NOT HttpOnly)
                //  - client echoes it in X-CSRF-Token header on state-changing requests
                var csrfToken = Guid.NewGuid().ToString("N");
                CookieHelper.SetCookie(httpContext.Response, "csrfToken", csrfToken, httpOnly: false);
            }

            return (username, _config.SecureMode);
        }
    }
}
