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
        //Field<CompanyType>("companyById")
        //    .Argument<NonNullGraphType<IdGraphType>>("id")
        //    .ResolveAsync(async context =>
        //    {
        //        using var scope = context.RequestServices!.CreateScope();
        //        var services = scope.ServiceProvider;
        //        var id = context.GetArgument<Guid>("id");
        //        return await services.GetRequiredService<ICompanyService>().GetCompany(id);
        //    });

        //Field<CompanyType>("company");
        Field<ListGraphType<EducationType>>("education").ResolveAsync(async ctx =>
        {
            using var scope = ctx.RequestServices!.CreateScope();
            var services = scope.ServiceProvider;
            var education = await services.GetRequiredService<IEducationService>().GetEducations(ctx.CancellationToken);
            return education;
        });
        //Field<ProjectType>("project");
        //Field<SkillType>("skill");
        //Field<UserType>("user");
    }
}