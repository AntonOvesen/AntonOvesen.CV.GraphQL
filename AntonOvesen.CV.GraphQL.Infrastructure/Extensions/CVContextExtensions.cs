using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure.Extensions;

public static class CVContextExtensions
{
    extension(DbContext context)
    {
        public async Task TrySeedContext(CancellationToken cancellationToken)
        {
            // Simple migration
            await context.Database.MigrateAsync(cancellationToken);

            var user = CreateUser();
            if (await context.TrySeed([user], cancellationToken))
            {
                var institution = CreateInstitution();
                if (await context.TrySeed([institution], cancellationToken))
                {
                    await context.TrySeed([CreateEducation(institution, user)], cancellationToken);
                }

                await context.TrySeed([CreateCompanies(user)], cancellationToken);

                await context.TrySeed([CreateProjects(user)], cancellationToken);

                await context.TrySeed([CreateSkills(user)], cancellationToken);
            }
        }

        private async Task<bool> TrySeed<TEntity>(List<TEntity> data, CancellationToken cancellationToken) where TEntity : class
        {
            var set = context.Set<TEntity>();

            if (await set.AnyAsync(cancellationToken))
            {
                return false;
            }

            set.AddRange(data);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

    private static List<Skill> CreateSkills(User user) => [
        new Skill {
            User = user,
            Title = "Asp.Net",
            Description = "Dotnet Web Application Development",
        },
        new Skill {
            User = user,
            Title = "Virtual Actors",
            Description = "Dapr.Actors & Microsoft.Orleans",
        },
        new Skill {
            User = user,
            Title = "MySql w/ EFCore",
            Description = "Working with MySql through EntityFramework Core"
        },
        new Skill {
            User = user,
            Title = "MongoDB",
            Description = "MongoDB and its vast featureset of aggregation pipelines"
        },
        new Skill {
            User = user,
            Title = "Memgraph/Cypher/Neo4j",
            Description = "Integrating to Memgraph/Neo4j, writing cypher queries etc."
        },
        new Skill {
            User = user,
            Title = "Event based architecture",
            Description = "Using Redis streams & Dapr Pubsub"
        },
        new Skill {
            User = user,
            Title = "Angular",
            Description = "Standalone applications and embedded in .Net framework MVC"
        },
        new Skill {
            User = user,
            Title = "GitOps CI/CD",
            Description = "Github Actions"
        },
        new Skill {
            User = user,
            Title = "Kubernets",
            Description = "AKS, Argocd, Helm, Working with, deploying to and configuring."
        }
    ];

    private static List<Project> CreateProjects(User user) => [
        new Project {
            Title = "Roo Wrath of Gilgamesh",
            Description = "Joint game making project. Published on itch.io",
            Ref = "https://kasperdev.itch.io/roo-wrath-of-gilgamesh",
            User = user
        },
        new Project {
            Title = "Chroma",
            Description = "Joint game making project. Published on steam",
            Ref = "https://store.steampowered.com/app/1332490/Chroma/",
            User = user
        },
        new Project {
            Title = "Guid Converter",
            Description = "Lille Blazor webapp til at base64 en-og-decode Guid's",
            Ref = "https://github.com/AntonOvesen/services-utilities"
        }
    ];

    private static List<Company> CreateCompanies(User user) => [
        new Company
        {
            Name = "Tryg CardsAffinity",
            From = DateTime.Parse("2023-01-15T00:00:00.000Z"),
            Role = "Backend Developer",
            Website = "https://affinity.tryg.dk/",
            User = user
        },
        new Company
        {
            Name = "Tryg Affinity",
            From = DateTime.Parse("2022-08-01T00:00:00.000Z"),
            To = DateTime.Parse("2023-01-15T00:00:00.000Z"),
            Role = "Backend Developer Praktik",
            Website = "https://affinity.tryg.dk/",
            User = user
        },
        new Company
        {
            Name = "Videndjurs",
            From = DateTime.Parse("2020-08-01T00:00:00.000Z"),
            To = DateTime.Parse("2022-06-01T00:00:00.000Z"),
            Role = "Underviser Spiludvikling (Deltid)",
            Website = "https://videndjurs.dk/",
            User = user
        },
    ];

    private static Education CreateEducation(Institution institution, User user) => new Education
    {
        Institution = institution,
        User = user,
        Level = "Erhvervsakademisk Uddannelse",
        Name = "Datamatiker"
    };

    private static Institution CreateInstitution() => new Institution
    {
        Name = "EA Dania Grenaa",
        Website = "https://eadania.dk"
    };

    private static User CreateUser() => new User
    {
        FirstName = "Anton",
        LastName = "Ovesen",
        Email = "anton@ovesen-mail.dk",
        Phone = new Phone
        {
            CountryCode = "+45",
            Number = "51929765"
        }
    };
}