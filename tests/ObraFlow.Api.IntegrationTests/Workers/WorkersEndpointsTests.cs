using System.Net;
using System.Net.Http.Json;
using FluentAssertions;

namespace ObraFlow.Api.IntegrationTests.Workers;

public class WorkersEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public WorkersEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetWorkers_ShouldReturnOk()
    {
        // Act
        var response = await _client.GetAsync("/workers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task PostWorker_ShouldReturnCreated()
    {
        // Arrange
        var newWorker = new
        {
            name = "Adrian Test",
            role = "Electrician",
            phoneNumber = "1234567",
            hourlyRate = 25.50m,
            isActive = true
        };

        // Act
        var response = await _client.PostAsJsonAsync("/workers", newWorker);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}