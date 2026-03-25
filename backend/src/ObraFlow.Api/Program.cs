using System.Text.Json;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using ObraFlow.Api.Middleware;
using ObraFlow.Application.Abstractions;
using ObraFlow.Infrastructure.Persistence;
using ObraFlow.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
const string FrontendCorsPolicy = "FrontendDevelopment";
const string DemoCreateWritesPolicy = "DemoCreateWrites";
var demoResetRequested = args.Any(arg =>
    string.Equals(arg, "reset-demo", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(arg, "--reset-demo", StringComparison.OrdinalIgnoreCase));
var forwardedHeadersEnabled = string.Equals(
    Environment.GetEnvironmentVariable("ASPNETCORE_FORWARDEDHEADERS_ENABLED"),
    "true",
    StringComparison.OrdinalIgnoreCase);
var allowedOrigins = GetAllowedOrigins(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy
                .WithOrigins(allowedOrigins)
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

if (forwardedHeadersEnabled)
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        options.KnownNetworks.Clear();
        options.KnownProxies.Clear();
    });
}

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/json";

        var payload = JsonSerializer.Serialize(new
        {
            message = "Demo write limit exceeded. Please wait a few minutes before trying again."
        });

        await context.HttpContext.Response.WriteAsync(payload, cancellationToken);
    };

    options.AddPolicy(DemoCreateWritesPolicy, httpContext =>
    {
        var rateLimitingEnabled = httpContext.RequestServices
            .GetRequiredService<IConfiguration>()
            .GetValue<bool>("DemoMode:EnableWriteRateLimiting");

        if (!rateLimitingEnabled)
        {
            return RateLimitPartition.GetNoLimiter("demo-create-writes-disabled");
        }

        var clientIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: clientIp,
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(10),
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                AutoReplenishment = true
            });
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("ObraFlow.Infrastructure");
        }));
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<DemoDatabaseResetService>();

var app = builder.Build();
var applyMigrationsOnStartup = app.Configuration.GetValue<bool>("Database:ApplyMigrationsOnStartup");

if (applyMigrationsOnStartup && !demoResetRequested)
{
    await ApplyMigrationsAsync(app.Services);
}

if (demoResetRequested)
{
    await using var scope = app.Services.CreateAsyncScope();
    var demoDatabaseResetService = scope.ServiceProvider.GetRequiredService<DemoDatabaseResetService>();

    Console.WriteLine("Resetting ObraFlow demo database...");
    var result = await demoDatabaseResetService.ResetAsync();
    Console.WriteLine(
        $"Demo database reset completed. Workers={result.WorkersCount}, DailyReports={result.DailyReportsCount}, Incidents={result.IncidentsCount}");

    return;
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (forwardedHeadersEnabled)
{
    app.UseForwardedHeaders();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseCors(FrontendCorsPolicy);
app.UseRateLimiter();

app.UseAuthorization();
app.MapGet("/health", CheckHealthAsync);
app.MapControllers();

app.Run();

static string[] GetAllowedOrigins(IConfiguration configuration)
{
    var configuredOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
    if (configuredOrigins is { Length: > 0 })
    {
        return configuredOrigins
            .Where(origin => !string.IsNullOrWhiteSpace(origin))
            .Select(origin => origin.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    var rawOrigins = configuration["Cors:AllowedOrigins"];
    if (string.IsNullOrWhiteSpace(rawOrigins))
    {
        return Array.Empty<string>();
    }

    return rawOrigins
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToArray();
}

static async Task ApplyMigrationsAsync(IServiceProvider services)
{
    await using var scope = services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

static async Task<IResult> CheckHealthAsync(AppDbContext dbContext, CancellationToken cancellationToken)
{
    try
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);

        return canConnect
            ? Results.Ok(new { status = "ok" })
            : Results.Problem(
                statusCode: StatusCodes.Status503ServiceUnavailable,
                title: "Database unavailable");
    }
    catch
    {
        return Results.Problem(
            statusCode: StatusCodes.Status503ServiceUnavailable,
            title: "Database unavailable");
    }
}

public partial class Program;
