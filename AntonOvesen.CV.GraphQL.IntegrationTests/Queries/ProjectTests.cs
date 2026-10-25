using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text.Json;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.Queries;

public class ProjectTests(ApiWebApplicationFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task ProjectsSearch_ProjectExists_CanReadAllData()
    {
        // Arrange
        var project = new Project
        {
            Title = "AntonOvesen.CV.GraphQL",
            Description = "Learning about GraphQL backend development",
            Ref = "./"
        };
        var user = new User
        {
            FirstName = "Anton",
            Projects = [project]
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = @"query { projects { id title description ref user { id } } }"
        };

        var expected = new GraphQLResponse<GraphQLProjectsResponse>
        {
            Data = new GraphQLProjectsResponse
            {
                Projects = [new Project {
                    Id = project.Id,
                    Title = project.Title,
                    Ref = project.Ref,
                    Description = project.Description,
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

        var result = JsonSerializer.Deserialize<GraphQLResponse<GraphQLProjectsResponse>>(responseString, JsonSerializerOptions.Web);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    #region Models

    public record GraphQLProjectsResponse
    {
        public List<Project>? Projects { get; set; }
    }

    #endregion Models
}