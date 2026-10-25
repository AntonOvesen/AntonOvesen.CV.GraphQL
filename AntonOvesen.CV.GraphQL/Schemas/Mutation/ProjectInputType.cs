using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class ProjectMutations : ObjectGraphType
{
    public ProjectMutations()
    {
        Field<ProjectType>("create")
            .Argument<NonNullGraphType<ProjectInputType>>("project")
            .ResolveAsync(async ctx =>
            {
                var projectRepository = ctx.RequestServices!.GetRequiredService<IProjectRepository>();
                return await projectRepository.Create(ctx.GetArgument<Project>("project"), ctx.CancellationToken);
            });

        Field<ProjectType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var projectRepository = ctx.RequestServices!.GetRequiredService<IProjectRepository>();
                return await projectRepository.Delete(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
    }
}

public class ProjectInputType : InputObjectGraphType<Project>
{
    public ProjectInputType()
    {
        Name = "ProjectInput";
        Field(x => x.Title).Description("Title or name of the project");
        Field(x => x.Description).Description("Describes the project");
        Field(x => x.Ref).Description("Link or other reference to the project");
        Field(x => x.UserId).Description("Whom his project belongs to");
    }
}