using CsrfDemoApi.Services;
using CsrfDemoApi.Filters;

var builder = WebApplication.CreateBuilder(args);// Only wires DI, CORS, MVC pipeline


// Add services to the container.

// === CORS: allow Angular dev app at http://localhost:4200 ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("NgDev", p =>
        p.WithOrigins("http://localhost:4200", "http://localhost:8000")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials());
});

// MVC controllers
builder.Services.AddControllers();

// === DI registrations ===
// In-memory "session" for demo. Replace with real auth/DB in prod.
builder.Services.AddSingleton<ISessionStore, InMemorySessionStore>();
builder.Services.AddSingleton<IAuthService, AuthService>();

// Toggle secure mode for CSRF demo in ONE place
builder.Services.AddSingleton<ICsrfConfig>(new CsrfConfig(secureMode: true)); // or false

var app = builder.Build();

// Pipeline
app.UseCors("NgDev");
app.MapControllers();

app.Run();
