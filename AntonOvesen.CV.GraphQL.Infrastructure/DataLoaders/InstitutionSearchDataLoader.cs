using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class InstitutionSearchDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<string, List<Institution>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Institution>>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Institutions
                .Where(education => EF.Functions.Like(education.Name, searchTerm))
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}

public class InstitutionDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, Institution>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, Institution>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Institutions.Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id, cancellationToken);
        foreach (var item in list)
        {
            var result = data.TryGetValue(item.Key, out var dataItem) ? dataItem : null;
            item.SetResult(result); // Ignore nullable warning i guess...
        }
    }
}