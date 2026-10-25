using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class InstitutionMutations : ObjectGraphType
{
    public InstitutionMutations()
    {
        Field<InstitutionType>("create")
            .Argument<NonNullGraphType<InstitutionInputType>>("institution")
            .ResolveAsync(async ctx =>
            {
                var educationRepository = ctx.RequestServices!.GetRequiredService<IEducationRepository>();
                return await educationRepository.CreateInstitution(ctx.GetArgument<Institution>("institution"), ctx.CancellationToken);
            });

        Field<InstitutionType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var educationRepository = ctx.RequestServices!.GetRequiredService<IEducationRepository>();
                return await educationRepository.DeleteInstitution(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
    }
}

public class InstitutionInputType : InputObjectGraphType<Institution>
{
    public InstitutionInputType()
    {
        Name = "InstitutionInput";
        Field(x => x.Name).Description("Name of the institution");
        Field(x => x.Website).Description("Link to the institutions website");
    }
}