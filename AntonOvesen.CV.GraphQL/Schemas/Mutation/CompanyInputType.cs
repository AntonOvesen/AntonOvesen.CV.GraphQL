using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class CompanyInputType : InputObjectGraphType<Company>
{
    public CompanyInputType()
    {
        Name = "CompanyInput"
        Field(x => x.Name).Description("The current name of the company");
        Field(x => x.Founded).Description("When was the company founded");
        Field(x => x.Website).Description("Link to the companies website");
        Field(x => x.From).Description("When the user started at this company");
        Field(x => x.To).Description("When the user departed the company. Empty if active employer");
        Field(x => x.UserId).Description("User employed at this company");
    }
}
