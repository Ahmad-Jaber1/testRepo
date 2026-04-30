// AuditLoggingMiddleware.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class AuditLoggingMiddleware
{
    private readonly RequestDelegate _next;

    
    public AuditLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<AuditLoggingMiddleware> logger)
    {
       
        var clientAppId = context.Items["ClientAppId"]?.ToString() ?? "Unknown";

        var traceId = Guid.NewGuid().ToString();

        logger.LogInformation("ClientAppId: {ClientAppId}, TraceId: {TraceId}", clientAppId, traceId);

        context.Response.Headers["TraceId"] = traceId;

        await _next(context);
    }
}

// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<AuditLoggingMiddleware>();

app.MapControllers();

app.Run();


// DiagnosticsController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/diagnostics")]
public class DiagnosticsController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus([FromQuery] string checkType)
    {
        if (string.IsNullOrWhiteSpace(checkType))
        {
            return BadRequest("checkType is required.");
        }

        return Ok(new { message = $"Check '{checkType}' completed successfully." });
    }
}
