using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ObraFlow.Api.IntegrationTests.Operations;

public class SwaggerEndpointsTests
{
    private static WebApplicationFactory<Program> CreateSwaggerEnabledFactory()
    {
        return new CustomWebApplicationFactory().WithConfigurationOverrides(new Dictionary<string, string?>
        {
            ["Swagger:Enabled"] = "true"
        });
    }

    private static HttpClient CreateClient(WebApplicationFactory<Program> factory)
    {
        return factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [Fact]
    public async Task SwaggerEndpoints_ShouldServeUiAndJson_WhenEnabled()
    {
        using var factory = CreateSwaggerEnabledFactory();
        using var client = CreateClient(factory);

        var indexResponse = await client.GetAsync("/swagger/index.html");
        var jsonResponse = await client.GetAsync("/swagger/v1/swagger.json");

        indexResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        jsonResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        (await indexResponse.Content.ReadAsStringAsync()).Should().Contain("Swagger UI");
        (await jsonResponse.Content.ReadAsStringAsync()).Should().Contain("\"openapi\"");
    }
}
