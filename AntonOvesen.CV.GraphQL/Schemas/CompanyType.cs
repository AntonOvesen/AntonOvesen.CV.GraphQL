using GraphQL.Types;
using AntonOvesen.CV.GraphQL.Core;

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
            .Resolve(ctx => ctx.Source.User)
            .Description("User employed at this company");
        Field(x => x.Skills, typeof(ListGraphType<SkillType>))
            .Resolve(ctx => ctx.Source.Skills)
            .Description("Skills aquired and applied at this company");
    }
}