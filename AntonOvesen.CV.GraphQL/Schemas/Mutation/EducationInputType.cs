using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class EducationMutations : ObjectGraphType
{
    public EducationMutations()
    {
        Field<EducationType>("create")
            .Argument<NonNullGraphType<EducationInputType>>("education")
            .ResolveAsync(async ctx =>
            {
                var educationRepository = ctx.RequestServices!.GetRequiredService<IEducationRepository>();
                return await educationRepository.Create(ctx.GetArgument<Education>("education"), ctx.CancellationToken);
            });

        Field<EducationType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var educationRepository = ctx.RequestServices!.GetRequiredService<IEducationRepository>();
                return await educationRepository.Delete(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
    }
}

public class EducationInputType : InputObjectGraphType<Education>
{
    public EducationInputType()
    {
        Name = "EducationInput";
        Field(x => x.Name).Description("Name or type of education");
        Field(x => x.Level).Description("Education level. e.g. \"Erhvervsakademiuddannelse\", \"Kandidat\" etc");
        Field(x => x.InstitutionId).Description("Institution this education was completed at");
        Field(x => x.UserId).Description("User who completed this education");
    }
}