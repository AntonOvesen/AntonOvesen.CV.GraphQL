using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class CompanyType : ObjectGraphType<Company>
{
    public CompanyType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Name).Description("The current name of the company");
        Field(x => x.Founded).Description("When was the company founded");
        Field(x => x.Website).Description("Link to the companies website");
        Field(x => x.From).Description("When the user started at this company");
        Field(x => x.To).Description("When the user departed the company. Empty if active employer");
        Field(x => x.User, typeof(UserType))
            .Description("User employed at this company")
            .ResolveAsync(ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserDataLoader>();
                return loader.LoadAsync(ctx.Source.UserId);
            });
        Field(x => x.Skills, typeof(ListGraphType<SkillType>))
            .Description("Skills aquired and applied at this company");
    }
}