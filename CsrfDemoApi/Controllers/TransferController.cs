using CsrfDemoApi.Filters;
using CsrfDemoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CsrfDemoApi.Controllers;

/// <summary>
/// Protected endpoint:
/// - Requires a valid session (SessionRequired)
/// - If secure mode ON, requires matching CSRF token (ValidateCsrf)
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    [HttpPost]
    [SessionRequired]
    [ValidateCsrf]
    public IActionResult Post([FromBody] TransferDto dto)
    {
        var amount = dto.Amount;
        var username = HttpContext.Items["username"]?.ToString() ?? "unknown";
        // If we got here, session + (optionally) CSRF checks are passed.
        return Ok(new ApiResponse($"User {username} transferred ${amount} successfully", Secure: true));
    }
}