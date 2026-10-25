using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

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
            Founded = DateTime.Parse("2002-01-28"),
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
            Query = @"query { companies { id name founded from to website user { id } } }"
        };

        var expected = new GraphQLResponse<GraphQLCompaniesResponse>
        {
            Data = new GraphQLCompaniesResponse
            {
                Companies = [new Company {
                    Id = company.Id,
                    Founded = company.Founded,
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

    #region Models

    public record GraphQLCompaniesResponse
    {
        public List<Company>? Companies { get; set; }
    }

    #endregion Models
}