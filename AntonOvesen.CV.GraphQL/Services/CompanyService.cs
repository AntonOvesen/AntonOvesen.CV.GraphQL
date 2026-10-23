using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Services;

public interface ICompanyService
{
    Task<Company> GetCompany(Guid companyId);
}

public class CompanyService : ICompanyService
{
    // Todo: add infrastructure later
    public Task<Company> GetCompany(Guid companyId)
    {
        return Task.FromResult(new Company
        {
            Id = companyId,
            Name = "Tryg CardsAffinity",
            Founded = DateTime.Parse("2002-01-01"),
            Website = "tryg.dk"
        });
    }
}