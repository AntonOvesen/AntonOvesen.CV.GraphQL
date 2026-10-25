using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IProjectRepository
{
    Task<Project> Create(Project project, CancellationToken cancellationToken);

    Task<Project> Delete(Guid id, CancellationToken cancellationToken);
}

public class ProjectRepository(CVContext context) : IProjectRepository
{
    public async Task<Project> Create(Project project, CancellationToken cancellationToken)
    {
        context.Projects.Add(project);
        await context.SaveChangesAsync(cancellationToken);
        return project;
    }

    public async Task<Project> Delete(Guid id, CancellationToken cancellationToken)
    {
        var Project = await context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (Project == null)
        {
            return null;
        }
        context.Projects.Remove(Project);
        await context.SaveChangesAsync(cancellationToken);
        return Project;
    }
}