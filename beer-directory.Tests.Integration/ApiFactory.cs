using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace beer_directory.Tests.Integration;

public class ApiFactory : WebApplicationFactory<IWebAssemblyMarker>, IAsyncLifetime
{
    public HttpClient HttpClient { get; protected set; } = default!;

    public async Task InitializeAsync()
    {
        // Setup test db here..

        HttpClient = GetHttpClient();
    }

    public async Task DisposeAsync()
    {
    }

    protected HttpClient GetHttpClient()
    {
        var clientFactory = WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // ...
            });
        });

        var client = clientFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        return client;
    }
}