using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IUserRepository
{
    Task<User> Create(User user, CancellationToken cancellationToken);

    Task<User?> Delete(Guid id, CancellationToken cancellationToken);
}

public class UserRepository(CVContext context) : IUserRepository
{
    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        context.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user == null)
        {
            return null;
        }
        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }
}