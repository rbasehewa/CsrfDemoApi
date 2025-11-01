namespace CsrfDemoApi.Utils
{
    /// <summary>
    /// Central helper to set cookies with consistent flags.
    /// IMPORTANT: For HTTPS environments, set Secure = true.
    /// </summary>
    public static class CookieHelper
    {
        public static void SetCookie(HttpResponse res, string name, string value, bool httpOnly)
        {
            res.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = httpOnly,   // JS cannot read HttpOnly cookies (good for sessionId)
                Secure = false,        // DEV ONLY: false on http://. Use true on HTTPS.
                SameSite = SameSiteMode.Lax, // Good default; consider Strict for highly sensitive flows
                MaxAge = TimeSpan.FromDays(1),
                Path = "/"
            });
        }
    }
}