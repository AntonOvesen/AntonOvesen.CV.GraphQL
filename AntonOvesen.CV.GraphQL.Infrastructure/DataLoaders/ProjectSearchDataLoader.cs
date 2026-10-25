using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class ProjectSearchDataLoader(CVContext context) : DataLoaderBase<string, List<Project>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Project>>> list, CancellationToken cancellationToken)
    {
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Projects
                .Where(project =>
                    EF.Functions.Like(project.Title, searchTerm) ||
                    EF.Functions.Like(project.Description, searchTerm)
                )
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}