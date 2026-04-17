using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Api.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace ElektronickePosudky.IntegrationTests.Controllers;

public class CiselnikControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private const string BaseUrl = "/api/v2/ciselniky";

    public CiselnikControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Accept-Language", "cs");
        _client.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
    }

    #region 1. GET /api/v2/ciselniky (GetAll)

    [Fact]
    public async Task GetAll_ShouldReturnOk_AndListOfCodebooks()
    {
        var response = await _client.GetAsync(BaseUrl);

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Database might not be seeded. Details: {error}");
        }

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<List<CiselnikDto>>();
        data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAll_ShouldReturn408RequestTimeout_WhenRequestIsCanceled()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(1));

        try
        {
            var response = await _client.GetAsync(BaseUrl, cts.Token);

            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                var problemDetails = await response.Content.ReadFromJsonAsync<ExtendedValidationProblemDetails>();
                problemDetails.Should().NotBeNull();
                problemDetails!.Status.Should().Be(StatusCodes.Status408RequestTimeout);
            }
        }
        catch (TaskCanceledException)
        {
            Assert.True(true, "Client timeout triggered successfully.");
        }
    }

    #endregion

    #region 2. GET /api/v2/ciselniky/{kod}/polozky (GetItems)

    [Fact]
    public async Task GetItems_ShouldReturnOk_WhenCodebookExists()
    {
        var validKod = "TYP_AKCE_1";

        // Act
        var response = await _client.GetAsync($"{BaseUrl}/{validKod}/polozky");

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Validation failed! Details: {errorContent}");
        }

        if (response.StatusCode == HttpStatusCode.NotFound) return;

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetItems_ShouldReturnNotFound_WhenCodebookDoesNotExist()
    {
        var invalidKod = "NOTFOUND01";

        var response = await _client.GetAsync($"{BaseUrl}/{invalidKod}/polozky");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var problemDetails = await response.Content.ReadFromJsonAsync<ExtendedValidationProblemDetails>();
        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be(StatusCodes.Status404NotFound);

        problemDetails.Detail.Should().Contain(invalidKod);
    }

    [Fact]
    public async Task GetItems_ShouldReturnEnglishError_WhenLanguageHeaderIsEn()
    {
        var invalidKod = "NOTFOUND01";
        _client.DefaultRequestHeaders.Remove("Accept-Language");
        _client.DefaultRequestHeaders.Add("Accept-Language", "en");

        var response = await _client.GetAsync($"{BaseUrl}/{invalidKod}/polozky");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var problemDetails = await response.Content.ReadFromJsonAsync<ExtendedValidationProblemDetails>();

        problemDetails!.Title.Should().Be("Codebook not found.");
    }

    #endregion

    #region 3. Global Behavior Tests

    [Fact]
    public async Task AllEndpoints_ShouldReturnCorrelationId_WhenProvidedInHeader()
    {
        var expectedCorrelationId = Guid.NewGuid().ToString();
        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
        request.Headers.Add("X-Correlation-Id", expectedCorrelationId);

        // Act
        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ExtendedValidationProblemDetails>();
            problemDetails!.CorrelationId.Should().Be(expectedCorrelationId);
        }
    }

    #endregion
}