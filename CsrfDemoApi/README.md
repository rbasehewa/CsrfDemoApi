# CSRF vs XSS Demo — CsrfDemoApi

**Simple demo** showing how Cross-Site Request Forgery (CSRF) works and how a server-side double-submit token blocks it.

---

## What is CSRF (one line)
An attacker forces a logged-in user’s browser to send requests (cookies are sent automatically). Server must verify a token to make sure requests are legitimate.

## Note


1. an auth cookie (e.g. sessionId, HttpOnly) used to authenticate the user, and

2. a CSRF cookie (e.g. csrfToken, readable by JS).
On every state-changing request the client must send the csrf value again (usually in X-CSRF-Token header). The server verifies (a) the user is authenticated (sessionId valid) and (b) the header value equals the csrf cookie value — only then it accepts the request.


---

## What’s in this repo
- `CsrfDemoApi/` — .NET Web API (backend)
- `evil.html` — attacker page (static)
- `cookies.txt` — example cookie jar for curl tests

Place this `README.md` at the repo root. GitHub will show it on the repo page.

---

## Quick steps (run the demo)

### 1) Start backend (API)
Open terminal in `CsrfDemoApi` folder:

**PowerShell / CMD**
```powershell
cd CsrfDemoApi
dotnet run --urls "http://localhost:5187"

2) Serve attacker page (Use Node)

From folder with evil.html

npx http-server -p 8000
# open http://localhost:8000/evil.html

3) Demo flow
	Login -> curl.exe -i --cookie-jar cookies.txt -H "Content-Type: application/json" --data "{\"username\":\"ryan\"}" http://localhost:5187/api/auth/login

	Open evil.html in browser (http://localhost:8000/evil.html
).

- If CSRF protection is OFF (see toggle below), the POST will succeed (200).

- If CSRF protection is ON, the POST will be blocked (403).

	
4. Toggle demo mode

Open Program.cs and change this single line:

builder.Services.AddSingleton<ICsrfConfig>(new CsrfConfig(secureMode: true)); // true = protected, false = vulnerable

** secureMode: false → demonstration shows vulnerability (attack works)

** secureMode: true → server rejects forged requests (attack blocked)