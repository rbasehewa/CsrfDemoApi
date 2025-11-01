namespace CsrfDemoApi.Services;

/// <summary>
/// Central flag to switch CSRF protection on/off for the demo.
/// In real apps, read from config/environment.
/// </summary>
public interface ICsrfConfig
{
    bool SecureMode { get; }
}

public class CsrfConfig : ICsrfConfig
{
    public CsrfConfig(bool secureMode) => SecureMode = secureMode;
    public bool SecureMode { get; }
}
