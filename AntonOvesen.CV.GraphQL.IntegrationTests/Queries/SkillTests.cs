using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class SkillTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task SkillsSearch_SkillExists_CanReadAllData()
    {
        // Arrange
        var skill = new Skill
        {
            Title = "Dotnet",
            Description = "Programming in dotnet"
        };
        var user = new User
        {
            FirstName = "John",
            Skills = [skill]
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = @"query { skills { id title description user { id } } }"
        };

        var expected = new GraphQLResponse<GraphQLSkillsResponse>
        {
            Data = new GraphQLSkillsResponse
            {
                Skills = [new Skill {
                    Id = skill.Id,
                    Title = skill.Title,
                    Description = skill.Description,
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

        var result = JsonSerializer.Deserialize<GraphQLResponse<GraphQLSkillsResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task SkillCreate_ValidRequest_CreatesSkill()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task SkillDelete_SkillExists_DeletesSkillFromDatabase()
    {
        throw new NotImplementedException();
    }

    #region Models

    public record GraphQLSkillsResponse
    {
        public List<Skill>? Skills { get; set; }
    }

    #endregion Models
}