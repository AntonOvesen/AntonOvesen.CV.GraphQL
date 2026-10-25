using AntonOvesen.CV.GraphQL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Setup;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Mock host services here.
            var cvContextDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(IDbContextFactory<CVContext>));
            if (cvContextDescriptor != null)
            {
                services.Remove(cvContextDescriptor);
            }
            services.AddDbContextFactory<CVContext>(options =>
            {
                options.UseMySql(MySqlFixture.ConnectionString, new MySqlServerVersion("8.0.26"));
            });
        });

        base.ConfigureWebHost(builder);
    }
}