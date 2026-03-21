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

    private static async Task<DailyReportDto> CreateDailyReportAsync(HttpClient client, Guid workerId, string description)
    {
        var newDailyReport = new
        {
            date = new DateOnly(2026, 3, 21),
            workerId,
            hoursWorked = 8.5m,
            description
        };

        var response = await client.PostAsJsonAsync("/daily-reports", newDailyReport);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdReport = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        createdReport.Should().NotBeNull();

        return createdReport!;
    }

    [Fact]
    public async Task GetDailyReports_ShouldReturnOk()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client, "Report Worker");
        await CreateDailyReportAsync(client, worker.Id, "Initial report");

        var response = await client.GetAsync("/daily-reports");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var reports = await response.Content.ReadFromJsonAsync<List<DailyReportDto>>();
        reports.Should().NotBeNull();
        reports.Should().NotBeEmpty();
    }

    [Fact]
    public async Task PostDailyReport_ShouldReturnCreated()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client, "Create Report Worker");

        var newDailyReport = new
        {
            date = new DateOnly(2026, 3, 21),
            workerId = worker.Id,
            hoursWorked = 7.5m,
            description = "Concrete pouring completed"
        };

        var response = await client.PostAsJsonAsync("/daily-reports", newDailyReport);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var createdReport = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        createdReport.Should().NotBeNull();
        createdReport!.Id.Should().NotBe(Guid.Empty);
        createdReport.Date.Should().Be(newDailyReport.date);
        createdReport.WorkerId.Should().Be(worker.Id);
        createdReport.WorkerName.Should().Be(worker.Name);
        createdReport.HoursWorked.Should().Be(newDailyReport.hoursWorked);
        createdReport.Description.Should().Be(newDailyReport.description);
    }

    [Fact]
    public async Task GetDailyReports_ShouldReturnCreatedReport()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client, "List Report Worker");
        var createdReport = await CreateDailyReportAsync(client, worker.Id, "Safety inspection completed");

        var response = await client.GetAsync("/daily-reports");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var reports = await response.Content.ReadFromJsonAsync<List<DailyReportDto>>();
        reports.Should().NotBeNull();
        reports.Should().Contain(x => x.Id == createdReport.Id && x.WorkerName == worker.Name);
    }

    [Fact]
    public async Task GetDailyReportById_ShouldReturnCreatedReport()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client, "Get Report Worker");
        var createdReport = await CreateDailyReportAsync(client, worker.Id, "Materials received");

        var response = await client.GetAsync($"/daily-reports/{createdReport.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var report = await response.Content.ReadFromJsonAsync<DailyReportDto>();
        report.Should().NotBeNull();
        report!.Id.Should().Be(createdReport.Id);
        report.WorkerId.Should().Be(worker.Id);
        report.WorkerName.Should().Be(worker.Name);
        report.Description.Should().Be("Materials received");
    }

    [Fact]
    public async Task GetDailyReportById_ShouldReturn404_WhenNotFound()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var response = await client.GetAsync($"/daily-reports/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task PostDailyReport_ShouldReturnBadRequest_WhenInvalid()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var invalidDailyReport = new
        {
            date = default(DateOnly),
            workerId = Guid.Empty,
            hoursWorked = 0m,
            description = ""
        };

        var response = await client.PostAsJsonAsync("/daily-reports", invalidDailyReport);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var validationProblem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        validationProblem.Should().NotBeNull();
        validationProblem!.Errors.Should().ContainKey("Date");
        validationProblem.Errors.Should().ContainKey("WorkerId");
        validationProblem.Errors.Should().ContainKey("HoursWorked");
        validationProblem.Errors.Should().ContainKey("Description");
    }

    [Fact]
    public async Task PostDailyReport_ShouldReturnBadRequest_WhenWorkerDoesNotExist()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var newDailyReport = new
        {
            date = new DateOnly(2026, 3, 21),
            workerId = Guid.NewGuid(),
            hoursWorked = 8m,
            description = "Attempt with missing worker"
        };

        var response = await client.PostAsJsonAsync("/daily-reports", newDailyReport);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var validationProblem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        validationProblem.Should().NotBeNull();
        validationProblem!.Errors.Should().ContainKey("WorkerId");
    }

    [Fact]
    public async Task UpdateDailyReport_ShouldModifyReport()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var originalWorker = await CreateWorkerAsync(client, "Original Worker");
        var replacementWorker = await CreateWorkerAsync(client, "Replacement Worker");
        var createdReport = await CreateDailyReportAsync(client, originalWorker.Id, "Original report");

        var update = new
        {
            date = new DateOnly(2026, 3, 22),
            workerId = replacementWorker.Id,
            hoursWorked = 6.5m,
            description = "Updated report"
        };

        var putResponse = await client.PutAsJsonAsync($"/daily-reports/{createdReport.Id}", update);

        putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var updatedReport = await putResponse.Content.ReadFromJsonAsync<DailyReportDto>();
        updatedReport.Should().NotBeNull();
        updatedReport!.Date.Should().Be(update.date);
        updatedReport.WorkerId.Should().Be(replacementWorker.Id);
        updatedReport.WorkerName.Should().Be(replacementWorker.Name);
        updatedReport.HoursWorked.Should().Be(update.hoursWorked);
        updatedReport.Description.Should().Be(update.description);
    }

    [Fact]
    public async Task DeleteDailyReport_ShouldRemoveReport()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = CreateClient(factory);

        var worker = await CreateWorkerAsync(client, "Delete Report Worker");
        var createdReport = await CreateDailyReportAsync(client, worker.Id, "Delete me");

        var deleteResponse = await client.DeleteAsync($"/daily-reports/{createdReport.Id}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var getResponse = await client.GetAsync($"/daily-reports/{createdReport.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
