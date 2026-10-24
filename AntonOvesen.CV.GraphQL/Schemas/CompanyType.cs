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
        // TODO: flesh out relations and rest of fields
    }
}