using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            Title = "Deleting Skills",
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
            Query = @"query { skills(searchTerm: ""skill"") { id title description user { id } } }"
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
    create(skill: $skillData)
    {
      id
      title
      description
    }
  }
}",
            Variables = new
            {
                SkillData = newSkill
            }
        };

        var expected = new
        {
            newSkill.Description,
            newSkill.Title
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataSkill<DataSkillCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Skill!.Create!.Id.Should().NotBeEmpty();
        responseData.Data.Skill.Create.Should().BeEquivalentTo(expected);

        var skillInDatabase = await Context.Skills.FirstOrDefaultAsync(x => x.Id == responseData.Data.Skill.Create.Id, TestContext.Current.CancellationToken);
        skillInDatabase.Should().BeEquivalentTo(newSkill);
    }

    [Fact]
    public async Task SkillDelete_SkillExists_DeletesSkillFromDatabase()
    {
        // Arrange
        var skill = new Skill
        {
            Title = "Writing tests",
            Description = "You heard me!"
        };
        var user = new User
        {
            FirstName = "SkillDeleteUser",
            Skills = [skill]
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = $@"
mutation {{
    skill {{
        delete(id: ""{skill.Id}"") {{ id }}
    }}
}}
"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataSkill<DataSkillDelete>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        skill.Id.Should().NotBeEmpty();
        responseData!.Data!.Skill!.Delete!.Id.Should().Be(skill.Id);

        var skillExists = await Context.Skills.AnyAsync(x => x.Id == skill.Id, TestContext.Current.CancellationToken);
        skillExists.Should().BeFalse();
    }

    #region Models

    public record GraphQLSkillsResponse
    {
        public List<Skill>? Skills { get; set; }
    }

    public class DataSkillDelete
    {
        public Skill? Delete { get; set; }
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