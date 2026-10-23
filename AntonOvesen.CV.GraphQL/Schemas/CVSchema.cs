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
        Field<CompanyType>("company")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context =>
            {
                using var scope = context.RequestServices!.CreateScope();
                var services = scope.ServiceProvider;
                var id = context.GetArgument<Guid>("id");
                return await services.GetRequiredService<ICompanyService>().GetCompany(id);
            });
    }
}