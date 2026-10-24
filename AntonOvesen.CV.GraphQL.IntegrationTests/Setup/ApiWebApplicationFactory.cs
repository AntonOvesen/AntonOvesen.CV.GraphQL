using AntonOvesen.CV.GraphQL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Setup;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer mySqlContainer = new MySqlBuilder("mysql:8.0.26").Build();

    public async ValueTask InitializeAsync()
    {
        await mySqlContainer.StartAsync();
    }

    public new ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return mySqlContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Mock host services here.
            var cvContextDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(CVContext));
            if (cvContextDescriptor != null)
            {
                services.Remove(cvContextDescriptor);
            }
            services.AddMySql<CVContext>(mySqlContainer.GetConnectionString(), new MySqlServerVersion("8.0.26"));
        });

        base.ConfigureWebHost(builder);
    }
}