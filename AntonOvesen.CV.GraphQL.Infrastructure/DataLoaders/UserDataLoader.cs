using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class UserDataLoader(CVContext context) : DataLoaderBase<Guid, User>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, User>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Users.Include(x => x.Phone).Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id, cancellationToken);
        foreach (var item in list)
        {
            var result = data.TryGetValue(item.Key, out var dataItem) ? dataItem : null;
            item.SetResult(result); // Ignore nullable warning i guess...
        }
    }
}

public class UserSkillsDataLoader(CVContext context) : DataLoaderBase<Guid, List<Skill>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Skill>?>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Skills
            .Include(x => x.User)
            .Where(x => ids.Contains(x.UserId))
            .ToListAsync(cancellationToken);

        var lookup = data.ToLookup(x => x.UserId);
        foreach (var item in list)
        {
            var result = lookup[item.Key].ToList();
            item.SetResult(result);
        }
    }
}

public class UserEducationDataLoader(CVContext context) : DataLoaderBase<Guid, List<Education>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Education>?>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Educations
            .Include(x => x.User)
            .Where(x => ids.Contains(x.UserId))
            .ToListAsync(cancellationToken);

        var lookup = data.ToLookup(x => x.UserId);
        foreach (var item in list)
        {
            var result = lookup[item.Key].ToList();
            item.SetResult(result);
        }
    }
}

public class UserCompanyDataLoader(CVContext context) : DataLoaderBase<Guid, List<Company>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Company>?>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Companies
            .Include(x => x.User)
            .Where(x => ids.Contains(x.UserId))
            .ToListAsync(cancellationToken);

        var lookup = data.ToLookup(x => x.UserId);
        foreach (var item in list)
        {
            var result = lookup[item.Key].ToList();
            item.SetResult(result);
        }
    }
}

public class UserProjectDataLoader(CVContext context) : DataLoaderBase<Guid, List<Project>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Project>?>> list, CancellationToken cancellationToken)
    {
        var ids = list.Select(x => x.Key);
        var data = await context.Projects
            .Include(x => x.User)
            .Where(x => ids.Contains(x.UserId))
            .ToListAsync(cancellationToken);

        var lookup = data.ToLookup(x => x.UserId);
        foreach (var item in list)
        {
            var result = lookup[item.Key].ToList();
            item.SetResult(result);
        }
    }
}