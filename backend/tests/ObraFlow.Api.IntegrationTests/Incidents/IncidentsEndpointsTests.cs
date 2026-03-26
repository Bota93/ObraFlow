using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using ObraFlow.Application.DTOs.Incidents;
using ObraFlow.Domain.Enums;

namespace ObraFlow.Api.IntegrationTests.Incidents;

public class IncidentsEndpointsTests
{
    private static HttpClient CreateClient(CustomWebApplicationFactory factory)
    {
        return factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetIncidents_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync("/incidents");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var incidents = await response.Content.ReadFromJsonAsync<List<IncidentDto>>();
        incidents.Should().NotBeNull();
    }

    [Fact]
    public async Task PostIncident_ShouldReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var payload = new
        {
            title = $"Incident {Guid.NewGuid()}",
            description = "Water leak detected in floor 2.",
            status = IncidentStatus.Open,
            reportedAtUtc = DateTime.UtcNow
        };

        var response = await client.PostAsJsonAsync("/incidents", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var created = await response.Content.ReadFromJsonAsync<IncidentDto>();
        created.Should().NotBeNull();
        created!.Title.Should().Be(payload.title);
        created.Description.Should().Be(payload.description);
        created.Status.Should().Be(IncidentStatus.Open);

        var rawPayload = await response.Content.ReadAsStringAsync();
        rawPayload.Should().Contain("\"reportedAtUtc\":\"");
        rawPayload.Should().Contain("Z\"");
    }

    [Fact]
    public async Task GetIncidentById_ShouldReturnNotFound_WhenIncidentDoesNotExist()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync($"/incidents/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostIncident_ShouldReturnBadRequest_WhenPayloadIsInvalid()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var payload = new
        {
            title = "",
            description = "",
            status = IncidentStatus.Open,
            reportedAtUtc = DateTime.UtcNow
        };

        var response = await client.PostAsJsonAsync("/incidents", payload);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem.Should().NotBeNull();
    }

    [Fact]
    public async Task PostIncident_ShouldReturnBadRequest_WhenReportedAtUtcDoesNotUseExplicitUtc()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        using var content = CreateJsonContent(
            """
            {
              "title": "Incident without UTC suffix",
              "description": "Timestamp is missing the explicit UTC suffix.",
              "status": 1,
              "reportedAtUtc": "2026-03-21T09:00:00"
            }
            """);

        var response = await client.PostAsync("/incidents", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem.Should().NotBeNull();
        problem!.Errors.Should().ContainKey("ReportedAtUtc");
    }

    [Fact]
    public async Task PutIncident_ShouldUpdateIncident()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateIncidentAsync(client);

        var payload = new
        {
            title = $"Updated incident {Guid.NewGuid()}",
            description = "Issue escalated and under review.",
            status = IncidentStatus.InProgress,
            reportedAtUtc = DateTime.UtcNow.AddMinutes(-30)
        };

        var response = await client.PutAsJsonAsync($"/incidents/{created.Id}", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await response.Content.ReadFromJsonAsync<IncidentDto>();
        updated.Should().NotBeNull();
        updated!.Title.Should().Be(payload.title);
        updated.Description.Should().Be(payload.description);
        updated.Status.Should().Be(IncidentStatus.InProgress);
    }

    [Fact]
    public async Task PutIncident_ShouldReturnBadRequest_WhenReportedAtUtcUsesNonUtcOffset()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateIncidentAsync(client);

        using var content = CreateJsonContent(
            $$"""
            {
              "title": "Updated incident with offset",
              "description": "Offset timestamps must be normalized client-side before sending.",
              "status": 2,
              "reportedAtUtc": "2026-03-21T11:00:00+02:00"
            }
            """);

        var response = await client.PutAsync($"/incidents/{created.Id}", content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem.Should().NotBeNull();
        problem!.Errors.Should().ContainKey("ReportedAtUtc");
    }

    [Fact]
    public async Task DeleteIncident_ShouldReturnNoContent_AndThenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateIncidentAsync(client);

        var deleteResponse = await client.DeleteAsync($"/incidents/{created.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/incidents/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private static async Task<IncidentDto> CreateIncidentAsync(HttpClient client)
    {
        var payload = new
        {
            title = $"Incident {Guid.NewGuid()}",
            description = "Material delivery blocked due to access issue.",
            status = IncidentStatus.Open,
            reportedAtUtc = DateTime.UtcNow
        };

        var response = await client.PostAsJsonAsync("/incidents", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var incident = await response.Content.ReadFromJsonAsync<IncidentDto>();
        incident.Should().NotBeNull();

        return incident!;
    }

    private static StringContent CreateJsonContent(string payload)
    {
        return new StringContent(payload, Encoding.UTF8, "application/json");
    }
}
