using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class EducationTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task EducationsSearch_EducationExists_CanReadAllData()
    {
        // Arrange
        var institution = new Institution
        {
            Name = "MI6",
            Website = "https://www.sis.gov.uk/"
        };
        var user = new User
        {
            FirstName = "James",
            LastName = "Bond"
        };
        var education = new Education
        {
            Institution = institution,
            User = user,
            Level = "<CLASSIFIED>",
            Name = "Secret Agent",
        };

        Context.Educations.Add(education);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = @"query { educations { id level name user {id} institution {id} } }"
        };

        var expected = new GraphQLResponse<GraphQLEducationsResponse>
        {
            Data = new GraphQLEducationsResponse
            {
                Educations = [new Education {
                    Id = education.Id,
                    Name = education.Name,
                    Level = education.Level,
                    Institution = new Institution {
                        Id = institution.Id,
                    },
                    User = new User {
                        Id = user.Id,
                    }
                }]
            }
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<GraphQLResponse<GraphQLEducationsResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task EducationCreate_ValidRequest_CreatesEducation()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task EducationDelete_EducationExists_DeletesEducationFromDatabase()
    {
        throw new NotImplementedException();
    }

    #region Models

    public record GraphQLEducationsResponse
    {
        public List<Education>? Educations { get; set; }
    }

    #endregion Models
}