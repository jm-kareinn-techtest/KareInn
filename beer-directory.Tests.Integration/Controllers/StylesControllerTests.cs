using System.Net;
using beer_directory.Tests.Integration.Models;
using FluentAssertions;

namespace beer_directory.Tests.Integration.Controllers;


[Collection("Shared collection")]
public class StylesControllerTests
{
    private readonly HttpClient _client;

    public StylesControllerTests(ApiFactory factory)
    {
        _client = factory.HttpClient;
    }

    [Fact]
    public async Task Get_should_return_all_styles()
    {
        // Act
        var response = await _client.GetAsync("api/Styles");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var styles = await response.Content.ReadFromJsonAsync<Style[]>();
        styles.Should().HaveCount(8);
    }
}