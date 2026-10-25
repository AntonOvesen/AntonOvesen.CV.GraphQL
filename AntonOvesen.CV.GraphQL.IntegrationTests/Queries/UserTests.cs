using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using NSubstitute;
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
    public async Task UsersSearch_MatchesOneEntry_CanLoadRelateRessources()
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
            Level = "Erhvervsakademiuddannelse",
            Institution = institution
        };

        var project = new Project
        {
            Description = "Description",
            Ref = "github.com",
            Title = "My project"
        };

        var company = new Company
        {
            Founded = DateTime.Parse("2002-01-01"),
            From = DateTime.Parse("2024-01-01"),
            To = DateTime.Parse("2025-01-01"),
            Name = "Tryg",
            Website = "tryg.dk"
        };

        var skill = new Skill
        {
            Title = "Dotnet",
            Description = "Programming :)",
        };

        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@doe.com",
            Phone = new Phone
            {
                CountryCode = "+45",
                Number = "00000000"
            },
            Educations = [education],
            Projects = [project],
            Companies = [company],
            Skills = [skill],
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
  users(searchTerm: ""{searchTerm}"") {{
    id
    firstName
    lastName
    email
    phone {{
      number
      countryCode
    }}
    educations {{
      id
      name
      level
      user {{id}}
      institution {{
        id
        name
        website
      }}
    }}
    companies {{
      id
      name
      founded
      website
      from
      to
      user {{id}}
    }}
    skills {{
      id
      title
      description
      user {{id}}
    }}
    projects {{
    	id
      title
      description
      ref
      user {{id}}
    }}
  }}
}}
"
        };
        var deepUser = new User { FirstName = null, Id = user.Id };
        var expected = new User // Guid empty is a downside of deserializing the entity model :/
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Educations = [new Education {
                Id = education.Id,
                Name = education.Name,
                Level = education.Level,
                Institution = new Institution {
                    Id = institution.Id,
                    Name = institution.Name,
                    Website = institution.Website
                },
                User = deepUser,
            }],
            Companies = [new Company {
                Id = company.Id,
                Name = company.Name,
                From = company.From,
                To = company.To,
                Founded = company.Founded,
                Website = company.Website,
                User = deepUser
            }],
            Skills = [new Skill {
                Id = skill.Id,
                Title = skill.Title,
                Description = skill.Description,
                User = deepUser
            }],
            Projects = [new Project {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Ref = project.Ref,
                User = deepUser
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

    [Fact]
    public async Task UserSearch_CallsTwice_DoesntFail()
    {

    }
}