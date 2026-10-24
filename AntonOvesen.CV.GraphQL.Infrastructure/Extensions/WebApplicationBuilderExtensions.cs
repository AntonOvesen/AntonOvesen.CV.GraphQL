using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMySql<CVContext>(configuration.GetConnectionString("MySQL"), new MySqlServerVersion("8.0.26"));
    }
} 