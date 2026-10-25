using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IEducationRepository
{
    Task<Education> Create(Education education, CancellationToken cancellationToken);

    Task<Institution> CreateInstitution(Institution institution, CancellationToken cancellationToken);

    Task<Education?> Delete(Guid id, CancellationToken cancellationToken);

    Task<Institution?> DeleteInstitution(Guid institutionId, CancellationToken cancellationToken);
}

internal class EducationRepository(CVContext context) : IEducationRepository
{
    public async Task<Education> Create(Education education, CancellationToken cancellationToken)
    {
        context.Educations.Add(education);
        await context.SaveChangesAsync(cancellationToken);
        return education;
    }

    public async Task<Institution> CreateInstitution(Institution institution, CancellationToken cancellationToken)
    {
        context.Institutions.Add(institution);
        await context.SaveChangesAsync(cancellationToken);
        return institution;
    }

    public async Task<Education?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var education = await context.Educations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (education == null)
        {
            return null;
        }
        context.Educations.Remove(education);
        await context.SaveChangesAsync(cancellationToken);
        return education;
    }

    public async Task<Institution?> DeleteInstitution(Guid institutionId, CancellationToken cancellationToken)
    {
        var institution = await context.Institutions.FirstOrDefaultAsync(x => x.Id == institutionId, cancellationToken);
        if (institution == null)
        {
            return null;
        }
        context.Institutions.Remove(institution);
        await context.SaveChangesAsync(cancellationToken);
        return institution;
    }
}