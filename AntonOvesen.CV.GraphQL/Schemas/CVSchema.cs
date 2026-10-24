using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class CVSchema : Schema
{
    public CVSchema()
    {
        Query = new CVSchemaQuery();
    }
}

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
            .Argument<string>("searchTerm")
            .ResolveAsync(async ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserSearchDataLoader>();
                return loader.LoadAsync(ctx.GetArgument<string>("searchTerm"));
            });

        // TODO:
        // Companys
        // Educations
        // Projects
        // Skills
        // Phones
    }
}