using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ObraFlow.Application.DTOs.Workers;

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

    [Fact]
    public async Task GetWorkers_ShouldReturnWorkersList()
    {
        // Act
        var response = await _client.GetAsync("/workers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var workers = await response.Content.ReadFromJsonAsync<List<WorkerDto>>();
        workers.Should().NotBeNull();
    }
    [Fact]
    public async Task PostWorker_ShouldPersistWorker()
    {
        var newWorker = new
        {
            name = "Persist Test",
            role = "Plumber",
            phoneNumber = "1234567",
            hourlyRate = 30.00m,
            isActive = true
        };

        var postResponse = await _client.PostAsJsonAsync("/workers", newWorker);
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await postResponse.Content.ReadFromJsonAsync<WorkerDto>();

        var getResponse = await _client.GetAsync($"/workers/{created!.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetWorkerById_ShouldReturn404_WhenNotFound()
    {
        var id = Guid.NewGuid();

        var response = await _client.GetAsync($"/workers/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostWorker_ShouldReturnBadRequest_WhenInvalid()
    {
        var invalidWorker = new
        {
            name = "", // inválido
            role = "A",
            phoneNumber = "123",
            hourlyRate = 0m
        };

        var response = await _client.PostAsJsonAsync("/workers", invalidWorker);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateWorker_ShouldModifyWorker()
    {
        var newWorker = new
        {
            name = "Update Test",
            role = "Electrician",
            phoneNumber = "1234567",
            hourlyRate = 20m,
            isActive = true
        };

        var post = await _client.PostAsJsonAsync("/workers", newWorker);
        var created = await post.Content.ReadFromJsonAsync<WorkerDto>();

        var update = new
        {
            name = "Updated Name",
            role = "Supervisor",
            phoneNumber = "7654321",
            hourlyRate = 40m,
            isActive = false
        };

        var putResponse = await _client.PutAsJsonAsync($"/workers/{created!.Id}", update);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await putResponse.Content.ReadFromJsonAsync<WorkerDto>();
        updated!.Name.Should().Be("Updated Name");
    }

    [Fact]
    public async Task DeleteWorker_ShouldRemoveWorker()
    {
        var newWorker = new
        {
            name = "Delete Test",
            role = "Worker",
            phoneNumber = "1234567",
            hourlyRate = 20m,
            isActive = true
        };

        var post = await _client.PostAsJsonAsync("/workers", newWorker);
        var created = await post.Content.ReadFromJsonAsync<WorkerDto>();

        var deleteResponse = await _client.DeleteAsync($"/workers/{created!.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await _client.GetAsync($"/workers/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

}
