using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface ISkillRepository
{
    Task<Skill> Create(Skill skill, CancellationToken cancellationToken);

    Task<Skill?> Delete(Guid id, CancellationToken cancellationToken);
}

internal class SkillRepository(CVContext context) : ISkillRepository
{
    public async Task<Skill> Create(Skill skill, CancellationToken cancellationToken)
    {
        context.Skills.Add(skill);
        await context.SaveChangesAsync(cancellationToken);
        return skill;
    }

    public async Task<Skill?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var skill = await context.Skills.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (skill == null)
        {
            return null;
        }
        context.Skills.Remove(skill);
        await context.SaveChangesAsync(cancellationToken);
        return skill;
    }
}