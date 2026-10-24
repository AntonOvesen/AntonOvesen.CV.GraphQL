using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Services;
using GraphQL;
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
                using var scope = ctx.RequestServices!.CreateScope();
                var id = ctx.GetArgument<Guid>("id");

                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var user = await userService.GetUser(id, ctx.CancellationToken);
                return user;
            });

        Field<ListGraphType<UserType>>("users")
            .Argument<string>("searchTerm")
            .ResolveAsync(async ctx =>
            {
                using var scope = ctx.RequestServices!.CreateScope();
                var searchTerm = ctx.GetArgument<string>("searchTerm");

                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                var users = await userService.SearchUsers(searchTerm, ctx.CancellationToken);
                return users;
            });

        //Field<ListGraphType<EducationType>>("education").ResolveAsync(async ctx =>
        //{
        //    using var scope = ctx.RequestServices!.CreateScope();
        //    var services = scope.ServiceProvider;
        //    var education = await services.GetRequiredService<IEducationService>().GetEducations(ctx.CancellationToken);
        //    return education;
        //});

    }
}