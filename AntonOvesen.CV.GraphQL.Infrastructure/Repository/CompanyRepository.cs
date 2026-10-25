using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository;

public interface ICompanyRepository
{
    Task<Company> Create(Company company, CancellationToken cancellationToken);

    Task<Company?> Delete(Guid id, CancellationToken cancellationToken);
}

internal class CompanyRepository(CVContext context) : ICompanyRepository
{
    public async Task<Company> Create(Company company, CancellationToken cancellationToken)
    {
        context.Companies.Add(company);
        await context.SaveChangesAsync(cancellationToken);
        return company;
    }

    public async Task<Company?> Delete(Guid id, CancellationToken cancellationToken)
    {
        var company = await context.Companies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (company == null)
        {
            return null;
        }
        context.Companies.Remove(company);
        await context.SaveChangesAsync(cancellationToken);
        return company;
    }
}