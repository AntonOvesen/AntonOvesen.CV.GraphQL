using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class InstitutionTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task InstitutionsSearch_InstitutionExists_CanReadAllData()
    {
        // Arrange
        var institution = new Institution
        {
            Name = "Dania",
            Website = "https://eadania.dk"
        };
        Context.Institutions.Add(institution);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = @"query { institutions { id name website } }"
        };

        var expected = new GraphQLResponse<GraphQLInstitutionsResponse>
        {
            Data = new GraphQLInstitutionsResponse
            {
                Institutions = [new Institution {
                    Id = institution.Id,
                    Name = institution.Name,
                    Website = institution.Website
                }]
            }
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<GraphQLResponse<GraphQLInstitutionsResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task InstitutionCreate_ValidRequest_CreatesInstitution()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task InstitutionDelete_InstitutionExists_DeletesInstitutionFromDatabase() 
    {  
        throw new NotImplementedException(); 
    }

    #region Models

    public record GraphQLInstitutionsResponse
    {
        public List<Institution>? Institutions { get; set; }
    }

    #endregion Models
}