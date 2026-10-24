using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IUserRepository
{
    Task<User> CreateUser(User user, CancellationToken cancellationToken);
}

public class UserRepository(CVContext context) : IUserRepository
{
    public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
    {
        context.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }
}