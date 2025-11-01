using System.Collections.Concurrent;

namespace CsrfDemoApi.Services // Simple in-memory session store (demo only)
{

    public class InMemorySessionStore : ISessionStore
    {
        private readonly ConcurrentDictionary<string, string> _sessions = new();

        public void Set(string sessionId, string username) => _sessions[sessionId] = username;

        public bool TryGet(string sessionId, out string username) => _sessions.TryGetValue(sessionId, out username!);
    }
}
