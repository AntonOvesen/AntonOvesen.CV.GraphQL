using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Repository
{
    public interface ICompanyRepository
    {
        Task<Company> Create(Company company, CancellationToken cancellationToken);
    }

    internal class CompanyRepository(CVContext context) : ICompanyRepository
    {
        public Task<Company> Create(Company company, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}