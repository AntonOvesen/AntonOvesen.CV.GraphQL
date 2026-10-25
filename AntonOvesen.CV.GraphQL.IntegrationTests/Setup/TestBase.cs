using AntonOvesen.CV.GraphQL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Setup;

[Collection("mysql")]
public class TestBase : IClassFixture<ApiWebApplicationFactory>, IDisposable
{
    protected IServiceScope Scope;
    protected HttpClient Client;

    protected CVContext Context;

    public TestBase(ApiWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        Scope = factory.Server.Services.CreateScope();
        Context = Scope.ServiceProvider.GetRequiredService<CVContext>();
        Context.Database.Migrate(); // Simplest way to migrate for now.
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted(); // Make sure we get a fresh db on each test
        GC.SuppressFinalize(this);
    }
}