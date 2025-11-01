using CsrfDemoApi.Models;
using CsrfDemoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CsrfDemoApi.Controllers
{
    /// <summary>
    /// Issues cookies: sessionId (HttpOnly) and csrfToken (if secure mode).
    /// Demo-only "login" (no password) to focus on CSRF.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
                return BadRequest(new { message = "username required" });

            var (username, secure) = _auth.Login(HttpContext, dto.Username);
            return Ok(new ApiResponse($"Logged in as {username}", secure));
        }
    }
}