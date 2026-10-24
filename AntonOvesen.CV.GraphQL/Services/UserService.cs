using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Services;

public interface IUserService
{
    Task<User?> GetUser(Guid UserId, CancellationToken cancellationToken);

    Task<List<User>> SearchUsers(string searchTerm,  CancellationToken cancellationToken);
}

public class UserService(CVContext context) : IUserService
{
    public Task<User?> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        return context.Users
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public Task<List<User>> SearchUsers(string searchTerm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}