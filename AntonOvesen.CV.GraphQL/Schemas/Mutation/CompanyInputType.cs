using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class CompanyMutations : ObjectGraphType
{
    public CompanyMutations()
    {
        Field<CompanyType>("create")
            .Argument<NonNullGraphType<CompanyInputType>>("company")
            .ResolveAsync(async ctx =>
            {
                var companyRepository = ctx.RequestServices!.GetRequiredService<ICompanyRepository>();
                return await companyRepository.Create(ctx.GetArgument<Company>("company"), ctx.CancellationToken);
            });

        Field<CompanyType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var companyRepository = ctx.RequestServices!.GetRequiredService<ICompanyRepository>();
                return await companyRepository.Delete(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
    }
}

public class CompanyInputType : InputObjectGraphType<Company>
{
    public CompanyInputType()
    {
        Name = "CompanyInput";
        Field(x => x.Name).Description("The current name of the company");
        Field(x => x.Role).Description("The users role at this company");
        Field(x => x.Website).Description("Link to the companies website");
        Field(x => x.From).Description("When the user started at this company");
        Field(x => x.To).Description("When the user departed the company. Empty if active employer");
        Field(x => x.UserId).Description("User employed at this company");
    }
}