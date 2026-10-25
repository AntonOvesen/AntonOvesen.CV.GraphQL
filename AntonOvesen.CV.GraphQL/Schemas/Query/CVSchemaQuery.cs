using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Query;

public class CVSchemaQuery : ObjectGraphType
{
    public CVSchemaQuery()
    {
        Field<UserType>("user")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<Guid>("id"));
            });

        Field<ListGraphType<UserType>>("users")
            .Argument<string>("searchTerm", nullable: true)
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserSearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });

        Field<ListGraphType<SkillType>>("skills")
            .Argument<string>("searchTerm", nullable: true)
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<SkillSearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });

        Field<ListGraphType<CompanyType>>("companies")
            .Argument<string>("searchTerm", nullable: true)
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<CompanySearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });

        Field<ListGraphType<EducationType>>("educations")
            .Argument<string>("searchTerm", nullable: true)
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<EducationSearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });

        Field<ListGraphType<ProjectType>>("projects")
            .Argument<string>("searchTerm", nullable: true)
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<ProjectSearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });
    }
}