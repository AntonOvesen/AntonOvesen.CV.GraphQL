using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Json;
using System.Text.Json;
using static AntonOvesen.CV.GraphQL.IntegrationTests.Queries.SkillTests;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class CompanyTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task CompanysSearch_CompanyExists_CanReadAllData()
    {
        // Arrange
        var company = new Company
        {
            Name = "Tryg",
            Role = "Backend Developer",
            From = DateTime.Parse("2024-01-01"),
            To = DateTime.Parse("2025-01-01"),
            Website = "tryg.dk"
        };
        var user = new User
        {
            FirstName = "Anton",
            Companies = [company]
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = @"query { companies { id name role from to website user { id } } }"
        };

        var expected = new GraphQLResponse<GraphQLCompaniesResponse>
        {
            Data = new GraphQLCompaniesResponse
            {
                Companies = [new Company {
                    Id = company.Id,
                    Role = company.Role,
                    From = company.From,
                    To  = company.To,
                    Name = company.Name,
                    Website = company.Website,
                    User = new User {
                        Id = user.Id
                    }
                }]
            }
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<GraphQLResponse<GraphQLCompaniesResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task CompanyCreate_ValidRequest_CreatesCompany()
    {
        // Arrange
        var user = new User
        {
            FirstName = "john"
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var newCompany = new
        {
            Name = "Company",
            Website = "website.com",
            Role = "Employee :D",
            From = DateTime.Parse("2001-01-01"),
            To = DateTime.Parse("2002-01-01"),
            UserId = user.Id
        };

        var request = new
        {
            Variables = new
            {
                CompanyData = newCompany
            },
            Query = @"mutation createCompany($companyData: CompanyInput!){
company { create(company: $companyData) {
    id
    name
    website
    role
    from
    to
}}}",
        };

        var expected = new
        {
            newCompany.Name,
            newCompany.Website,
            newCompany.Role,
            newCompany.From,
            newCompany.To
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataCompany<DataCompanyCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Company!.Create!.Id.Should().NotBeEmpty();
        responseData.Data.Company.Create.Should().BeEquivalentTo(expected);

        var companyInDatabase = await Context.Companies.FirstOrDefaultAsync(x => x.Id == responseData!.Data!.Company!.Create!.Id, TestContext.Current.CancellationToken);
        companyInDatabase.Should().BeEquivalentTo(newCompany);
    }

    [Fact]
    public async Task CompanyDelete_CompanyExists_RemovesCompanyFromDatabase()
    {
        // Arrange
        var company = new Company
        {
            Name = "Test",
            Website = "Test.com",
            Role = "Role",
            From = DateTime.Parse("26-03-2019"),
            To = DateTime.Parse("26-03-2020")
        };
        var user = new User()
        {
            Companies = [company]
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = $@"mutation {{ company {{ 
delete(id: ""{company.Id}"") 
{{
    id
    name
    website
    role
    from
    to
}} }} }}"
        };

        var expected = new
        {
            company.Id,
            company.Name,
            company.Website,
            company.Role,
            company.From,
            company.To
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataCompany<DataCompanyDelete>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Company!.Delete.Should().BeEquivalentTo(expected);

        var companyExists = await Context.Companies
            .AnyAsync(
                x => x.Id == responseData!.Data!.Company!.Delete.Id, 
                TestContext.Current.CancellationToken);

        companyExists.Should().BeFalse();
    }

    #region Models

    public record DataCompanyCreate
    {
        public Company? Create { get; set; }
    }

    public record DataCompanyDelete
    {
        public Company? Delete { get; set; }
    }

    public record GraphQLDataCompany<TCompany>
    {
        public TCompany? Company { get; set; }
    }

    public record GraphQLCompaniesResponse
    {
        public List<Company>? Companies { get; set; }
    }

    #endregion Models
}