namespace Pubinno.Api.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    //private const string APIKEY = "demo-token";
    private readonly string _apiKey;
    public ApiKeyMiddleware(RequestDelegate next,IConfiguration _config)
    {
        _next = next;
        _apiKey = _config.GetValue<string>("API_KEY") ?? throw new ArgumentNullException("APIKEY not found in configuration");
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var key) ||
            key != _apiKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
