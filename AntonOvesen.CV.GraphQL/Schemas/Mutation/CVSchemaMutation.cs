using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class CVSchemaMutation : ObjectGraphType
{
    public CVSchemaMutation()
    {
        Field<UserType>("createUser")
            .Argument<NonNullGraphType<UserInputType>>("user")
            .ResolveAsync(async ctx =>
            {
                var userRepo = ctx.RequestServices!.GetRequiredService<IUserRepository>();
                return await userRepo.CreateUser(ctx.GetArgument<User>("user"), ctx.CancellationToken);
            });

        Field<Education>("createEducation")
            .Argument<NonNullGraphType<EducationInputType>>("education");

        Field<Skill>("createSkill");
        
        Field<Company>("createCompany");
        
        Field<Project>("createProject");
    }
}