using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface ISkillRepository
{
    Task<Skill> Create(Skill skill, CancellationToken cancellationToken);
}

internal class SkillRepository(CVContext context) : ISkillRepository
{
    public Task<Skill> Create(Skill skill, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}