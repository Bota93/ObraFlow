using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using ObraFlow.Application.DTOs.Workers;

namespace ObraFlow.Api.IntegrationTests.Workers;

public class WorkersEndpointsTests
{
    private static HttpClient CreateClient(CustomWebApplicationFactory factory)
    {
        return factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri("https://localhost")
        });
    }

    private static async Task<WorkerDto> CreateWorkerAsync(HttpClient client, string name)
    {
        var newWorker = new
        {
            name,
            role = "Electrician",
            phoneNumber = "1234567",
            hourlyRate = 25.50m,
            isActive = true
        };

        var response = await client.PostAsJsonAsync("/workers", newWorker);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdWorker = await response.Content.ReadFromJsonAsync<WorkerDto>();
        createdWorker.Should().NotBeNull();

        return createdWorker!;
    }

    [Fact]
    public async Task GetWorkers_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        // Act
        var response = await client.GetAsync("/workers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var workers = await response.Content.ReadFromJsonAsync<List<WorkerDto>>();
        workers.Should().NotBeNull();
        workers.Should().NotBeEmpty();
    }

    [Fact]
    public async Task PostWorker_ShouldReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

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
        var response = await client.PostAsJsonAsync("/workers", newWorker);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response.Headers.Location.Should().NotBeNull();

        var createdWorker = await response.Content.ReadFromJsonAsync<WorkerDto>();
        createdWorker.Should().NotBeNull();
        createdWorker!.Id.Should().NotBe(Guid.Empty);
        createdWorker.Name.Should().Be(newWorker.name);
        createdWorker.Role.Should().Be(newWorker.role);
        createdWorker.PhoneNumber.Should().Be(newWorker.phoneNumber);
        createdWorker.HourlyRate.Should().Be(newWorker.hourlyRate);
        createdWorker.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task GetWorkers_ShouldReturnWorkersList()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        // Act
        var response = await client.GetAsync("/workers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var workers = await response.Content.ReadFromJsonAsync<List<WorkerDto>>();
        workers.Should().NotBeNull();
        workers.Should().Contain(x => x.Name == "Miguel Torres");
        workers.Should().Contain(x => x.Name == "Laura Martinez");
        workers.Should().Contain(x => x.Name == "Carlos Ramirez");
    }

    [Fact]
    public async Task PostWorker_ShouldPersistWorker()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateWorkerAsync(client, "Persist Test");

        var getResponse = await client.GetAsync($"/workers/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var persistedWorker = await getResponse.Content.ReadFromJsonAsync<WorkerDto>();
        persistedWorker.Should().NotBeNull();
        persistedWorker!.Id.Should().Be(created.Id);
        persistedWorker.Name.Should().Be("Persist Test");
        persistedWorker.Role.Should().Be("Electrician");
        persistedWorker.PhoneNumber.Should().Be("1234567");
        persistedWorker.HourlyRate.Should().Be(25.50m);
        persistedWorker.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task GetWorkerById_ShouldReturn404_WhenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var id = Guid.NewGuid();

        var response = await client.GetAsync($"/workers/{id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostWorker_ShouldReturnBadRequest_WhenInvalid()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var invalidWorker = new
        {
            name = "", // inválido
            role = "A",
            phoneNumber = "123",
            hourlyRate = 0m
        };

        var response = await client.PostAsJsonAsync("/workers", invalidWorker);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var validationProblem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        validationProblem.Should().NotBeNull();
        validationProblem!.Errors.Should().ContainKey("Name");
        validationProblem.Errors.Should().ContainKey("Role");
        validationProblem.Errors.Should().ContainKey("PhoneNumber");
        validationProblem.Errors.Should().ContainKey("HourlyRate");
    }

    [Fact]
    public async Task UpdateWorker_ShouldModifyWorker()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateWorkerAsync(client, "Update Test");

        var update = new
        {
            name = "Updated Name",
            role = "Supervisor",
            phoneNumber = "7654321",
            hourlyRate = 40m,
            isActive = false
        };

        var putResponse = await client.PutAsJsonAsync($"/workers/{created.Id}", update);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await putResponse.Content.ReadFromJsonAsync<WorkerDto>();
        updated.Should().NotBeNull();
        updated!.Name.Should().Be("Updated Name");
        updated.Role.Should().Be("Supervisor");
        updated.PhoneNumber.Should().Be("7654321");
        updated.HourlyRate.Should().Be(40m);
        updated.IsActive.Should().BeFalse();

        var getResponse = await client.GetAsync($"/workers/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var persistedWorker = await getResponse.Content.ReadFromJsonAsync<WorkerDto>();
        persistedWorker.Should().NotBeNull();
        persistedWorker!.Name.Should().Be("Updated Name");
    }

    [Fact]
    public async Task DeleteWorker_ShouldRemoveWorker()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var created = await CreateWorkerAsync(client, "Delete Test");

        var deleteResponse = await client.DeleteAsync($"/workers/{created.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/workers/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
