using AntonOvesen.CV.GraphQL.Infrastructure;
using AntonOvesen.CV.GraphQL.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL;

public class Migrator(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            
            var context = scope.ServiceProvider.GetRequiredService<CVContext>();

            context.Database.Migrate();

            await context.TrySeedContext(cancellationToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
