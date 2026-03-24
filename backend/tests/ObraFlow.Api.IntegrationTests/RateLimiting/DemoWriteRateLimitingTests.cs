using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ObraFlow.Application.DTOs.Workers;
using ObraFlow.Domain.Enums;

namespace ObraFlow.Api.IntegrationTests.RateLimiting;

public class DemoWriteRateLimitingTests
{
    private static readonly Guid SeedWorkerId = Guid.Parse("6d4c130b-d3ec-4da3-a7c3-f8ff5df74f01");

    private static HttpClient CreateClient(WebApplicationFactory<Program> factory)
    {
        return factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("https://localhost")
        });
    }

    private static WebApplicationFactory<Program> CreateDemoModeFactory()
    {
        var baseFactory = new CustomWebApplicationFactory();

        return baseFactory.WithConfigurationOverrides(new Dictionary<string, string?>
        {
            ["DemoMode:EnableWriteRateLimiting"] = bool.TrueString
        });
    }

    private static object CreateWorkerRequest(string name)
    {
        return new
        {
            name,
            role = "Electrician",
            phoneNumber = "1234567",
            hourlyRate = 25.50m,
            isActive = true
        };
    }

    private static object CreateDailyReportRequest(int index)
    {
        return new
        {
            date = new DateOnly(2026, 3, 23),
            workerId = SeedWorkerId,
            hoursWorked = 8.0m,
            description = $"Demo rate limit report {index}"
        };
    }

    private static object CreateIncidentRequest(int index)
    {
        return new
        {
            title = $"Demo rate limit incident {index}",
            description = $"Demo rate limit incident description {index}.",
            status = IncidentStatus.Open,
            reportedAtUtc = new DateTime(2026, 3, 23, 12, 0, 0, DateTimeKind.Utc).AddMinutes(index)
        };
    }

    private static Task<HttpResponseMessage> PostProtectedWriteAsync(HttpClient client, string endpoint, int index)
    {
        return endpoint switch
        {
            "/workers" => client.PostAsJsonAsync(endpoint, CreateWorkerRequest($"Rate Limited {index}")),
            "/daily-reports" => client.PostAsJsonAsync(endpoint, CreateDailyReportRequest(index)),
            "/incidents" => client.PostAsJsonAsync(endpoint, CreateIncidentRequest(index)),
            _ => throw new ArgumentOutOfRangeException(nameof(endpoint), endpoint, "Unsupported protected endpoint.")
        };
    }

    private static async Task AssertRateLimitedAsync(HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);

        var payload = await response.Content.ReadFromJsonAsync<RateLimitedResponse>();
        payload.Should().NotBeNull();
        payload!.Message.Should().Be("Demo write limit exceeded. Please wait a few minutes before trying again.");
    }

    [Fact]
    public async Task PostWorker_ShouldNotBeRateLimited_WhenDemoModeIsDisabled()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        for (var index = 0; index < 6; index++)
        {
            var response = await client.PostAsJsonAsync("/workers", CreateWorkerRequest($"No Limit {index}"));
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }

    [Theory]
    [InlineData("/workers")]
    [InlineData("/daily-reports")]
    [InlineData("/incidents")]
    public async Task ProtectedCreateEndpoints_ShouldReturnTooManyRequests_WhenDemoModeIsEnabledAndLimitIsExceeded(string endpoint)
    {
        using var factory = CreateDemoModeFactory();
        using var client = CreateClient(factory);

        for (var index = 0; index < 5; index++)
        {
            var response = await PostProtectedWriteAsync(client, endpoint, index);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        var throttledResponse = await PostProtectedWriteAsync(client, endpoint, 5);

        await AssertRateLimitedAsync(throttledResponse);
    }

    [Fact]
    public async Task ProtectedCreateEndpoints_ShouldShareTheSameDemoWriteBudget_WhenDemoModeIsEnabled()
    {
        using var factory = CreateDemoModeFactory();
        using var client = CreateClient(factory);

        (await PostProtectedWriteAsync(client, "/workers", 0)).StatusCode.Should().Be(HttpStatusCode.Created);
        (await PostProtectedWriteAsync(client, "/daily-reports", 1)).StatusCode.Should().Be(HttpStatusCode.Created);
        (await PostProtectedWriteAsync(client, "/incidents", 2)).StatusCode.Should().Be(HttpStatusCode.Created);
        (await PostProtectedWriteAsync(client, "/workers", 3)).StatusCode.Should().Be(HttpStatusCode.Created);
        (await PostProtectedWriteAsync(client, "/incidents", 4)).StatusCode.Should().Be(HttpStatusCode.Created);

        var throttledResponse = await PostProtectedWriteAsync(client, "/daily-reports", 5);

        await AssertRateLimitedAsync(throttledResponse);
    }

    [Fact]
    public async Task GetWorkers_ShouldRemainUnrestricted_WhenDemoModeWriteRateLimitingIsEnabled()
    {
        using var factory = CreateDemoModeFactory();
        using var client = CreateClient(factory);

        for (var index = 0; index < 5; index++)
        {
            var response = await PostProtectedWriteAsync(client, "/workers", index);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        var throttledResponse = await PostProtectedWriteAsync(client, "/workers", 5);
        await AssertRateLimitedAsync(throttledResponse);

        var getResponse = await client.GetAsync("/workers");

        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var workers = await getResponse.Content.ReadFromJsonAsync<List<WorkerDto>>();
        workers.Should().NotBeNullOrEmpty();
    }

    private sealed class RateLimitedResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}
