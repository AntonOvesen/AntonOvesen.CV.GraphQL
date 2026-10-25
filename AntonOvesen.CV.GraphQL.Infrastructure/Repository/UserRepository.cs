using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IUserRepository
{
    Task<User> Create(User user, CancellationToken cancellationToken);
    Task<User> Delete(Guid id, CancellationToken cancellationToken);
}

public class UserRepository(CVContext context) : IUserRepository
{
    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        context.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task<User> Delete(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}