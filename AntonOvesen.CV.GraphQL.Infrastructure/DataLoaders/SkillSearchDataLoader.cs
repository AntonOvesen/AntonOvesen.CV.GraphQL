using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class SkillSearchDataLoader(CVContext context) : DataLoaderBase<string, List<Skill>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<Skill>>> list, CancellationToken cancellationToken)
    {
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Skills
                .Where(skill =>
                    EF.Functions.Like(skill.Title, searchTerm) ||
                    EF.Functions.Like(skill.Description, searchTerm))
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}