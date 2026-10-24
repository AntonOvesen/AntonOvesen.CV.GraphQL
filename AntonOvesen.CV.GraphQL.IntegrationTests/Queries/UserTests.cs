using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class UserTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task Users_DatabaseContainsUser_ReturnsUser()
    {
        // Arrange
        var skill = new Skill
        {
            Title = "Cooking",
            User = null,
        };
        var user = new User
        {
            FirstName = "John",
            Skills = [skill]
        };
        skill.User = user;
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = $@"{{
	user(id: ""{user.Id}""){{
    id
    firstName
    skills{{
      title
    }}
  }}
}}"
        };

        var expected = new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            Skills = [
                new Skill {
                    Title = skill.Title,
                    User = null
                }
            ]
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLUserResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data.Should().NotBeNull();
        responseData.Data.User.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task UsersSearch_MatchesOneEntry_CanLoadRelatedEducationInstitution()
    {
        // Arrange
        var institution = new Institution
        {
            Name = "EA Dania",
            Website = "https://eadania.dk"
        };

        var education = new Education
        {
            Name = "Datamatiker",
            Institution = institution
        };

        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Educations = [education]
        };

        var decoy = new User
        {
            FirstName = "Jane",
            LastName = "Doe"
        };

        Context.Users.AddRange([user, decoy]);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var searchTerm = "john";
        var request = new
        {
            Query = $@"
{{
	users(searchTerm:""{searchTerm}""){{
    firstName
    educations{{
      name
      institution{{
        name
        website
      }}
    }}
  }}
}}
"
        };
        var expected = new User // Guid empty is a downside of deserializing the entity model :/
        {
            Id = Guid.Empty,
            FirstName = user.FirstName,
            Educations = [new Education {
                Id = Guid.Empty,
                Name = education.Name,
                UserId = Guid.Empty,
                Institution = new Institution {
                    Id = Guid.Empty,
                    Name = institution.Name,
                    Website = institution.Website
                }
            }]
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLUsersResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Users.Should().BeEquivalentTo([expected]);
    }
}