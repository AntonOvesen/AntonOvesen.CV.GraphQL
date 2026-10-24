using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class UserSkillsDataLoader(CVContext context) : DataLoaderBase<Guid, IEnumerable<Skill>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, IEnumerable<Skill>>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Skills.Where(x => ids.Contains(x.User!.Id)).ToListAsync(cancellationToken);
        var lookup = data.ToLookup(x => x.Id);
        foreach (var item in list)
        {
            var result = lookup[item.Key];
            item.SetResult(result);
        }
    }
}