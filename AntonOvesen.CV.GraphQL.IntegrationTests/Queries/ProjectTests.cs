using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

    [Fact]
    public async Task ProjectCreate_ValidRequest_CreatesProject()
    {
        // Arrange
        var user = new User()
        {
            FirstName = "ProjectCreateUser"
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var newProject = new
        {
            Title = "TestProject",
            Description = "Description",
            Ref = "Here",
            UserId = user.Id
        };
        var request = new
        {
            Query = @"mutation createProject($projectData: ProjectInput!) {
project {
    create(project: $projectData) { id, title, description, ref }
}
}",
            Variables = new
            {
                ProjectData = newProject
            }
        };

        var expected = new
        {
            newProject.Title,
            newProject.Description,
            newProject.Ref
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataProject<DataProjectCreate>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Project!.Create!.Id.Should().NotBeEmpty();
        responseData.Data.Project.Create.Should().BeEquivalentTo(expected);

        var projectInDatabase = await Context.Projects.FirstOrDefaultAsync(x => x.Id == responseData.Data.Project.Create.Id, TestContext.Current.CancellationToken);
        projectInDatabase.Should().BeEquivalentTo(newProject);
    }

    [Fact]
    public async Task ProjectDelete_ProjectExists_DeletesProjectFromDatabase()
    {
        // Arrange
        var project = new Project
        {
            Title = "Title",
            Description = "Description",
            Ref = "Ref"
        };

        var user = new User
        {
            Projects = [project]
        };

        Context.Users.Add(user);
        await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var request = new
        {
            Query = $@"
mutation {{
    project {{
        delete(id: ""{project.Id}"") {{
            id
            title
            description
            ref
        }}
    }}
}}",
        };

        var expected = new
        {
            project.Id,
            project.Title,
            project.Description,
            project.Ref
        };

        // Act
        var response = await Client.PostAsJsonAsync("/graphql", request, TestContext.Current.CancellationToken);

        var responseString = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var responseData = JsonSerializer.Deserialize<GraphQLResponse<GraphQLDataProject<DataProjectDelete>>>(responseString, JsonSerializerOptions.Web);

        // Assert
        responseData!.Data!.Project!.Delete.Should().BeEquivalentTo(expected);

        var projectExists = await Context.Projects
            .AnyAsync(
                x => x.Id == responseData.Data.Project.Delete.Id,
                TestContext.Current.CancellationToken);
        projectExists.Should().BeFalse();
    }

    #region Models

    public record GraphQLProjectsResponse
    {
        public List<Project>? Projects { get; set; }
    }

    public record DataProjectDelete
    {
        public Project? Delete { get; set; }
    }

    public record DataProjectCreate
    {
        public Project? Create { get; set; }
    }

    private record GraphQLDataProject<TProject>
    {
        public TProject? Project { get; set; }
    }

    #endregion Models
}