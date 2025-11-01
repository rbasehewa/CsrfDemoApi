using System.Collections.Concurrent;

namespace CsrfDemoApi.Services;

/// <summary>
/// Extremely basic demo storage for "logged-in users", keyed by sessionId.
/// NEVER use this in production.
/// </summary>
public interface ISessionStore
{
    void Set(string sessionId, string username);
    bool TryGet(string sessionId, out string username);
}
