using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMySqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContextFactory<CVContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("MySQL");
            options.UseMySql($"{connectionString};Database=graphql", new MySqlServerVersion("8.0.26"));
        });
    }

    public static IServiceCollection AddDataLoaders(this IServiceCollection services)
    {
        services.AddScoped<UserDataLoader>();
        services.AddScoped<UserSearchDataLoader>();
        services.AddScoped<UserSkillsDataLoader>();
        services.AddScoped<UserEducationDataLoader>();
        services.AddScoped<UserCompanyDataLoader>();
        services.AddScoped<UserProjectDataLoader>();

        services.AddScoped<CompanySearchDataLoader>();
        services.AddScoped<EducationSearchDataLoader>();
        services.AddScoped<InstitutionSearchDataLoader>();
        services.AddScoped<ProjectSearchDataLoader>();
        services.AddScoped<SkillSearchDataLoader>();

        services.AddScoped<InstitutionDataLoader>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }
}