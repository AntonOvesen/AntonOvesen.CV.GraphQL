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