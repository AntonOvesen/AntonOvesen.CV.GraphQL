using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMySql<CVContext>(configuration.GetConnectionString("MySQL"), new MySqlServerVersion("8.0.26"));
    }

    public static IServiceCollection AddDataLoaders(this IServiceCollection services)
    {
        services.AddScoped<UserDataLoader>();
        services.AddScoped<UserSearchDataLoader>();
        services.AddScoped<UserCompanyDataLoader>();
        services.AddScoped<UserProjectDataLoader>();
        services.AddScoped<UserEducationDataLoader>();
        services.AddScoped<UserSkillsDataLoader>();

        

        return services;
    }
} 