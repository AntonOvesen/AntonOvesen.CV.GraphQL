using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IProjectRepository
{
    Task<Project> Create(Project project, CancellationToken cancellationToken);
}

public class ProjectRepository(CVContext context) : IProjectRepository
{
    public Task<Project> Create(Project project, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}