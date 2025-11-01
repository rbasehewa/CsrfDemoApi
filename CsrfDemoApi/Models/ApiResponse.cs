namespace CsrfDemoApi.Models;

/// <summary>
/// Simple response wrapper to keep payloads consistent.
/// </summary>
public record ApiResponse(string Message, bool Secure);