using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class CompanySearchDataLoader(CVContext context) : DataLoaderBase<string, List<Company>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Company>>> list, CancellationToken cancellationToken)
    {
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Companies
                .Where(company => EF.Functions.Like(company.Name, searchTerm))
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}