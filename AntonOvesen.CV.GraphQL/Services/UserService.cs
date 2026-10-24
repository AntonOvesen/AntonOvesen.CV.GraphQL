using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Services;

public interface IUserService
{
    Task<User> GetUser(Guid UserId, CancellationToken cancellationToken);

    Task<List<User>> SearchUsers(string searchTerm,  CancellationToken cancellationToken);
}

public class UserService : IUserService
{
    public Task<User> GetUser(Guid UserId, CancellationToken cancellationToken)
    {
        return Task.FromResult(new User
        {
            Id = UserId,
            FirstName = "John"
        });
    }

    public Task<List<User>> SearchUsers(string searchTerm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}