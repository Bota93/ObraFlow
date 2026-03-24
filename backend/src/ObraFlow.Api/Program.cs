using System.Text.Json;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using ObraFlow.Application.Abstractions;
using ObraFlow.Infrastructure.Persistence;
using ObraFlow.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
const string FrontendCorsPolicy = "FrontendDevelopment";
const string DemoCreateWritesPolicy = "DemoCreateWrites";
var demoResetRequested = args.Any(arg =>
    string.Equals(arg, "reset-demo", StringComparison.OrdinalIgnoreCase) ||
    string.Equals(arg, "--reset-demo", StringComparison.OrdinalIgnoreCase));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://localhost:4173",
                "http://127.0.0.1:4173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);
app.UseRateLimiter();

app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program;
