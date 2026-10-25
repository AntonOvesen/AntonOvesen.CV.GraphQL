using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class EducationSearchDataLoader(CVContext context) : DataLoaderBase<string, List<Education>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Education>>> list, CancellationToken cancellationToken)
    {
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Educations
                .Include(x => x.Institution)
                .Where(education =>
                    EF.Functions.Like(education.Name, searchTerm) ||
                    EF.Functions.Like(education.Level, searchTerm) ||
                    (education.Institution != null && EF.Functions.Like(education.Institution.Name, searchTerm))
                )
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}