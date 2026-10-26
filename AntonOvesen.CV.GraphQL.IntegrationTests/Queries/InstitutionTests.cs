using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using static AntonOvesen.CV.GraphQL.IntegrationTests.Queries.SkillTests;

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
        // Arrange
        var newInstitution = new
        {
            Name = "SomeName",
            Website = "SomeSite.com"
        };

        var request = new
        {
            Query = @"mutation createInstitution($institutionData: InstitutionInput!) { 
institution { 
    create(institution: $institutionData) { 
        id 
        name 
        website 
    } 
} 
}",
            Variables = new
            {
                InstitutionData = newInstitution
            }
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataInstitution<DataInstitutionCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Institution!.Create!.Id.Should().NotBeEmpty();
        responseData.Data.Institution.Create.Should().BeEquivalentTo(newInstitution);

        var institutionInDatabase = await Context.Institutions.FirstOrDefaultAsync(x => x.Id == responseData!.Data!.Institution!.Create!.Id, TestContext.Current.CancellationToken);
        institutionInDatabase.Should().BeEquivalentTo(newInstitution);
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

    public class DataInstitutionCreate
    {
        public Institution? Create { get; set; }
    }

    public class GraphQLDataInstitution<TInstitution>
    {
        public TInstitution? Institution { get; set; }
    }

    #endregion Models
}