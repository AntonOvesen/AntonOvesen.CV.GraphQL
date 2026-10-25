using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface IEducationRepository
{
    Task<Education> Create(Education education, CancellationToken cancellationToken);

    Task<Institution> CreateInstitution(Institution institution, CancellationToken cancellationToken);

    Task<Education> Delete(Guid id, CancellationToken cancellationToken);

    Task<Institution> DeleteInstitution(Guid institutionId, CancellationToken cancellationToken);
}

internal class EducationRepository(CVContext context) : IEducationRepository
{
    public Task<Education> Create(Education education, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Institution> CreateInstitution(Institution institution, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Education> Delete(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Institution> DeleteInstitution(Guid institutionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}