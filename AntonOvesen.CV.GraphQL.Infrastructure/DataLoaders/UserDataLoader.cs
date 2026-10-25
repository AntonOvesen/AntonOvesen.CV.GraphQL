using AntonOvesen.CV.GraphQL.Core;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;

public class UserDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, User>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, User>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Users.Include(x => x.Phone).Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id, cancellationToken);
        foreach (var item in list)
        {
            var result = data.TryGetValue(item.Key, out var dataItem) ? dataItem : null;
            item.SetResult(result); // Ignore nullable warning i guess...
        }
    }
}

public class UserSearchDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<string, List<User>>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<string, List<User>>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        // Todo: optimize maybe?
        // If you can mark the matches based on what search they match, then group by that.
        // This can be one query
        foreach (var item in list)
        {
            var searchTerm = $"%{item.Key}%";
            var usersMatches = await context.Users
                .Where(user =>
                    EF.Functions.Like(user.FirstName, searchTerm) ||
                    EF.Functions.Like(user.LastName, searchTerm))
                .ToListAsync(cancellationToken);

            item.SetResult(usersMatches);
        }
    }
}

public class UserSkillsDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, List<Skill>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Skill>?>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Skills
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

public class UserEducationDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, List<Education>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Education>?>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Educations
            .Include(x => x.Institution)
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

public class UserCompanyDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, List<Company>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Company>?>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Companies
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

public class UserProjectDataLoader(IDbContextFactory<CVContext> contextFactory) : DataLoaderBase<Guid, List<Project>?>
{
    protected override async Task FetchAsync(IEnumerable<DataLoaderPair<Guid, List<Project>?>> list, CancellationToken cancellationToken)
    {
        using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var ids = list.Select(x => x.Key);
        var data = await context.Projects
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