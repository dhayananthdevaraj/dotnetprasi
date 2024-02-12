using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using dotnetapp.Services;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"];
        var token = authorizationHeader.ToString().Replace("Bearer ", string.Empty);

        if (!string.IsNullOrEmpty(token) && AuthService.ValidateJwt(token))
        {
            await _next.Invoke(context);
        }
        else
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid or expired token");
        }
    }
}
