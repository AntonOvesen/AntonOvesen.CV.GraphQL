using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests;

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

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();
        
        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLUserResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data.Should().NotBeNull();
        responseData.Data.User.Should().BeEquivalentTo(expected);
    }
}