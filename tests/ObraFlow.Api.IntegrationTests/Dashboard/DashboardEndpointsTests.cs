using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ObraFlow.Application.DTOs.Dashboard;

namespace ObraFlow.Api.IntegrationTests.Dashboard;

public class DashboardEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DashboardEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetSummary_ShouldReturnOk_WithExpectedMetrics()
    {
        var response = await _client.GetAsync("/dashboard/summary");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var summary = await response.Content.ReadFromJsonAsync<DashboardSummaryDto>();
        summary.Should().NotBeNull();

        summary!.TotalWorkers.Should().BeGreaterThan(0);
        summary.ActiveWorkers.Should().BeGreaterThanOrEqualTo(0);
        summary.ActiveWorkers.Should().BeLessThanOrEqualTo(summary.TotalWorkers);

        summary.TotalDailyReports.Should().BeGreaterThanOrEqualTo(0);

        summary.OpenIncidents.Should().BeGreaterThanOrEqualTo(0);
        summary.InProgressIncidents.Should().BeGreaterThanOrEqualTo(0);
        summary.ResolvedIncidents.Should().BeGreaterThanOrEqualTo(0);

        summary.HoursWorkedToday.Should().BeGreaterThanOrEqualTo(0);
        summary.HoursWorkedLast7Days.Should().BeGreaterThanOrEqualTo(summary.HoursWorkedToday);
    }
}