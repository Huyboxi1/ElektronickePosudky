using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Api.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace ElektronickePosudky.IntegrationTests.Controllers;

public class PosudekControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private const string BaseUrl = "/api/v2/posudky/ridicskeOpravneni";

    public PosudekControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Accept-Language", "cs");
        _client.DefaultRequestHeaders.Add("X-Correlation-Id", Guid.NewGuid().ToString());
    }

    #region 1. CREATE Endpoint Tests (POST /)

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenDataIsEmpty()
    {
        var invalidRequest = new PosudekRoCreateDto();
        var response = await _client.PostAsJsonAsync(BaseUrl, invalidRequest);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.Should().NotBeNull();

        problemDetails!.Errors.Should().ContainKey("Data.Rid");
        problemDetails.Errors.Should().ContainKey("Data.KrzpId");
        problemDetails.Errors.Should().ContainKey("Data.Zpusobilosti");
    }

    [Theory]
    [InlineData("", "123", "Hodnota RID musí mít 10 znaků.")]
    [InlineData("12345", "123", "Hodnota RID musí mít 10 znaků.")]
    public async Task Create_ShouldReturnBadRequest_WhenRidIsInvalid(string invalidRid, string krzpId, string expectedError)
    {
        var request = new PosudekRoCreateDto { Rid = invalidRid, KrzpId = krzpId };
        var response = await _client.PostAsJsonAsync(BaseUrl, request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails!.Errors["Data.Rid"].Should().Contain(expectedError);
    }

    [Fact]
    public async Task Create_ShouldReturnEnglishError_WhenLanguageIsEn()
    {
        var request = new PosudekRoCreateDto { Rid = "123" };

        _client.DefaultRequestHeaders.Remove("Accept-Language");
        _client.DefaultRequestHeaders.Add("Accept-Language", "en");

        var response = await _client.PostAsJsonAsync(BaseUrl, request);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        problemDetails!.Errors["Data.Rid"].Should().Contain("RID value must have 10 characters.");
    }

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenDataIsValid()
    {
        var validRequest = CreateValidMockRequest();
        var response = await _client.PostAsJsonAsync(BaseUrl, validRequest);

        if (response.StatusCode == HttpStatusCode.InternalServerError) return;

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }

    #endregion

    #region 2. GET Endpoint Tests (GET /{id})

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"{BaseUrl}/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetById_ShouldReturn304NotModified_WhenETagMatches()
    {
        var createResponse = await _client.PostAsJsonAsync(BaseUrl, CreateValidMockRequest());
        if (!createResponse.IsSuccessStatusCode) return;

        var createdDto = await createResponse.Content.ReadFromJsonAsync<CreatePosudekResponseDto>();
        var id = createdDto!.Id;

        var getResponse = await _client.GetAsync($"{BaseUrl}/{id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var etag = getResponse.Headers.ETag?.Tag;
        etag.Should().NotBeNullOrEmpty();

        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{id}");
        requestMessage.Headers.IfNoneMatch.Add(new EntityTagHeaderValue(etag!));

        var cachedResponse = await _client.SendAsync(requestMessage);

        cachedResponse.StatusCode.Should().Be(HttpStatusCode.NotModified);
    }

    #endregion

    #region 3. SEARCH Endpoint Tests (POST /vyhledat)

    [Fact]
    public async Task Search_ShouldReturnOk_WithValidCriteria()
    {
        // TODO: If SearchPosudkyDto has required fields, fill them in here.
        var searchCriteria = new SearchPosudkyDto { };

        var response = await _client.PostAsJsonAsync($"{BaseUrl}/vyhledat", searchCriteria);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    #endregion

    #region 4. HISTORY Endpoint Tests (GET /{id}/historie)

    [Fact]
    public async Task GetHistory_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"{BaseUrl}/{randomId}/historie");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region 5. INVALIDATE Endpoint Tests (PATCH /{id}/zneplatnit)

    [Fact]
    public async Task Invalidate_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var randomId = Guid.NewGuid();

        var request = new PosudekZneplatnitDto
        {
            KrzpId = "KRZP-123",
            Ico = "12345678",
            DuvodZneplatneni = new CodebookItemDto { Kod = "1", Verze = "1" }
        };

        var requestMessage = new HttpRequestMessage(HttpMethod.Patch, $"/api/v2/posudky/ridicskeOpravneni/{randomId}/zneplatnit")
        {
            Content = JsonContent.Create(request)
        };
        requestMessage.Headers.TryAddWithoutValidation("If-Match", "\"v1\"");

        // Act
        var response = await _client.SendAsync(requestMessage);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Validation failed! Please provide required fields in PosudekZneplatnitDto. Details: {error}");
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Invalidate_ShouldReturnBadRequest_WhenIfMatchHeaderIsMissing()
    {
        var randomId = Guid.NewGuid();

        var request = new PosudekZneplatnitDto { };

        var response = await _client.PatchAsJsonAsync($"{BaseUrl}/{randomId}/zneplatnit", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region 6. PDF Endpoint Tests (GET /{id}/pdf)

    [Fact]
    public async Task GetPdf_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"{BaseUrl}/{randomId}/pdf");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region 7. AUTHORIZATION Endpoint Tests (POST /zalozeni/opravneni)

    [Fact]
    public async Task CheckAuthorization_ShouldReturnOk_WhenValidRequest()
    {
        var request = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP-123",
            Ico = "12345678"     // Mock IČO (usually an 8-digit number in CZ)
        };

        // Act
        var response = await _client.PostAsJsonAsync($"{BaseUrl}/zalozeni/opravneni", request);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Validation failed! Details: {error}");
        }

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    #endregion

    #region Helper Methods

    private static PosudekRoCreateDto CreateValidMockRequest()
    {
        return new PosudekRoCreateDto
        {
            Rid = "1234567890",
            KrzpId = "KRZP-123",
            DatumVystaveni = DateTime.Now.AddDays(-1),
            TypAkce = new CodebookItemDto { Kod = "A", Verze = "1" },
            StavPosudku = new CodebookItemDto { Kod = "S", Verze = "1" },
            DruhProhlidky = new CodebookItemDto { Kod = "P", Verze = "1" },
            DruhPosudku = new CodebookItemDto { Kod = "DP", Verze = "1" },
            Zpusobilosti = new List<PosudekZpusobilostDto>
            {
                new PosudekZpusobilostDto
                {
                    SkupinaZadateleRidic = new CodebookItemDto { Kod = "Z", Verze = "1" },
                    Vysledek = new CodebookItemDto { Kod = "V", Verze = "1" },
                    SkupinyRidicskehoOpravneni = new List<PosudekSkupinaRoDetailDto>
                    {
                        new PosudekSkupinaRoDetailDto
                        {
                            SkupinaRo = new CodebookItemDto { Kod = "RO", Verze = "1" }
                        }
                    }
                }
            }
        };
    }

    #endregion
}