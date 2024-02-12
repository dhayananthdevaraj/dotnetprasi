using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using dotnetapp.Services;
using Newtonsoft.Json;

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
        if (context.Request.Path.StartsWithSegments("/auth/api/auth/login") ||
                  context.Request.Path.StartsWithSegments("/auth/api/auth/signup"))
        {
            await _next(context);
            return;
        }
        if (!string.IsNullOrEmpty(token) && AuthService.ValidateJwt(token))
        {
            await _next.Invoke(context);
        }
        else
        {
        context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var responseMessage = new
            {
                message = "Invalid or expired token"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseMessage));
            return;
        }
    }
}

// [16:02] Dhayananth D
// git remote add origin https://github.com/dhayananthdevaraj/jobdotnet.git

// git branch -M main

// git push -u origin main