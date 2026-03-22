using System.Net;
using System.Net.Http.Json;
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
}
