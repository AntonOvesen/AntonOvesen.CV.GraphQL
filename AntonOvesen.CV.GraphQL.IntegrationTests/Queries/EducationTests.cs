using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        // Arrange
        var institution = new Institution
        {
            Name = "Waaa"
        };
        var user = new User
        {
            FirstName = "Name"
        };
        Context.Institutions.Add(institution);
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var newEducation = new
        {
            Name = "My School",
            Level = "High",
            InstitutionId = institution.Id,
            UserId = user.Id,
        };

        var request = new
        {
            Query = @"mutation createEducation($educationData: EducationInput!) {
education {
    create(education: $educationData) {
        id
        name
        level
    }
}
}",
            Variables = new
            {
                EducationData = newEducation
            }
        };

        var expected = new
        {
            newEducation.Name,
            newEducation.Level
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataEducation<DataEducationCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Education!.Create!.Id.Should().NotBeEmpty();
        responseData.Data.Education.Create.Should().BeEquivalentTo(expected);

        var EducationInDatabase = await Context.Educations.FirstOrDefaultAsync(x => x.Id == responseData!.Data!.Education!.Create!.Id, TestContext.Current.CancellationToken);
        EducationInDatabase.Should().BeEquivalentTo(newEducation);
    }

    [Fact]
    public async Task EducationDelete_EducationExists_DeletesEducationFromDatabase()
    {
        // Arrange
        var user = new User();
        var institution = new Institution();
        var education = new Education
        {
            Institution = institution,
            User = user,
            Level = "Level",
            Name = "name",
        };
        Context.Educations.Add(education);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = $@"mutation {{ education {{
delete(id: ""{education.Id}"") {{
id
name
level
}} }} }}"
        };

        var expected = new
        {
            education.Id,
            education.Name,
            education.Level,
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataEducation<DataEducationDelete>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Education!.Delete.Should().BeEquivalentTo(expected);

        var companyExists = await Context.Educations
            .AnyAsync(
                x => x.Id == responseData!.Data!.Education!.Delete.Id,
                TestContext.Current.CancellationToken);
        companyExists.Should().BeFalse();
    }

    #region Models

    public record DataEducationDelete
    {
        public Education? Delete { get; set; }
    }

    public record DataEducationCreate
    {
        public Education? Create { get; set; }
    }

    private record GraphQLDataEducation<TEducation>
    {
        public TEducation? Education { get; set; }
    }

    public record GraphQLEducationsResponse
    {
        public List<Education>? Educations { get; set; }
    }

    #endregion Models
}