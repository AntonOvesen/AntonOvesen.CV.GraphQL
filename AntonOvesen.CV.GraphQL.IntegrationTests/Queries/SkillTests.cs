using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static AntonOvesen.CV.GraphQL.IntegrationTests.Queries.UserTests;

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
        // Arrange
        var user = new User()
        {
            FirstName = "Test"
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var newSkill = new
        {
            Title = "TestSkill",
            Description = "Description",
            UserId = user.Id
        };
        var request = new
        {
            Query = @"mutation createSkill($skillData: SkillInput!) {
  skill {
    create(skill: $skillData) { id user { id } }
  }
}",
            Variables = new
            {
                SkillData = newSkill
            }
        };


        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataSkill<DataSkillCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data.Should().NotBeNull();
        responseData.Data.Skill.Should().NotBeNull();
        responseData.Data.Skill.Create.Should().NotBeNull();

        responseData.Data.Skill.Create.User.Should().NotBeNull();
        responseData.Data.Skill.Create.Title.Should().Be(newSkill.Title);
        responseData.Data.Skill.Create.User.Id.Should().Be(user.Id);
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

    public class DataSkillCreate
    {
        public Skill? Create { get; set; }
    }

    private class GraphQLDataSkill<TSkill>
    {
        public TSkill? Skill { get; set; }
    }

    #endregion Models
}