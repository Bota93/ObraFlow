using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using ObraFlow.Application.DTOs.DailyReports;
using ObraFlow.Application.DTOs.Workers;

namespace ObraFlow.Api.IntegrationTests.DailyReports;

public class DailyReportsEndpointsTests
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
    public async Task GetDailyReports_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync("/daily-reports");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var reports = await response.Content.ReadFromJsonAsync<List<DailyReportDto>>();
        reports.Should().NotBeNull();
    }

    [Fact]
    public async Task PostDailyReport_ShouldReturnCreated_WhenPayloadIsValid()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client);

        var payload = new
        {
            date = DateOnly.FromDateTime(DateTime.UtcNow),
            workerId = worker.Id,
            hoursWorked = 8.5m,
            description = $"Daily report {Guid.NewGuid()}"
        };

        var response = await client.PostAsJsonAsync("/daily-reports", payload);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var created = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        created.Should().NotBeNull();
        created!.WorkerId.Should().Be(worker.Id);
        created.WorkerName.Should().Be(worker.Name);
        created.HoursWorked.Should().Be(8.5m);
    }

    [Fact]
    public async Task GetDailyReportById_ShouldReturnNotFound_WhenReportDoesNotExist()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync($"/daily-reports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostDailyReport_ShouldReturnBadRequest_WhenPayloadIsInvalid()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var payload = new
        {
            date = DateOnly.FromDateTime(DateTime.UtcNow),
            workerId = Guid.NewGuid(),
            hoursWorked = 0m,
            description = ""
        };

        var response = await client.PostAsJsonAsync("/daily-reports", payload);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var validationProblem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        validationProblem.Should().NotBeNull();
        validationProblem!.Errors.Should().ContainKey("HoursWorked");
        validationProblem.Errors.Should().ContainKey("Description");
    }

    [Fact]
    public async Task PutDailyReport_ShouldUpdateExistingReport()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client);
        var createdReport = await CreateDailyReportAsync(client, worker.Id);

        var updatedWorker = await CreateWorkerAsync(client);

        var payload = new
        {
            date = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1),
            workerId = updatedWorker.Id,
            hoursWorked = 6.25m,
            description = $"Updated report {Guid.NewGuid()}"
        };

        var response = await client.PutAsJsonAsync($"/daily-reports/{createdReport.Id}", payload);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        updated.Should().NotBeNull();
        updated!.WorkerId.Should().Be(updatedWorker.Id);
        updated.WorkerName.Should().Be(updatedWorker.Name);
        updated.HoursWorked.Should().Be(6.25m);
        updated.Description.Should().Be(payload.description);
    }

    [Fact]
    public async Task DeleteDailyReport_ShouldReturnNoContent_AndThenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client);
        var createdReport = await CreateDailyReportAsync(client, worker.Id);

        var deleteResponse = await client.DeleteAsync($"/daily-reports/{createdReport.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/daily-reports/{createdReport.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private static async Task<WorkerDto> CreateWorkerAsync(HttpClient client)
    {
        var payload = new
        {
            name = $"Worker {Guid.NewGuid()}",
            role = "Operator",
            phoneNumber = "1234567",
            hourlyRate = 25.50m,
            isActive = true
        };

        var response = await client.PostAsJsonAsync("/workers", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var worker = await response.Content.ReadFromJsonAsync<WorkerDto>();
        worker.Should().NotBeNull();

        return worker!;
    }

    private static async Task<DailyReportDto> CreateDailyReportAsync(HttpClient client, Guid workerId)
    {
        var payload = new
        {
            date = DateOnly.FromDateTime(DateTime.UtcNow),
            workerId,
            hoursWorked = 8m,
            description = $"Seed report {Guid.NewGuid()}"
        };

        var response = await client.PostAsJsonAsync("/daily-reports", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var report = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        report.Should().NotBeNull();

        return report!;
    }
}
