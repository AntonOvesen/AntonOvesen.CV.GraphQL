using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class EducationSearchDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<string, List<Education>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Education>>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Educations
                .Where(education =>
                    EF.Functions.Like(education.Name, searchTerm) ||
                    EF.Functions.Like(education.Level, searchTerm))
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}