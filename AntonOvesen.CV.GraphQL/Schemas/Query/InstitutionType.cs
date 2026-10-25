using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Query;

public class InstitutionType : ObjectGraphType<Institution>
{
    public InstitutionType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Name).Description("Name of the institution");
        Field(x => x.Website).Description("Link to the institutions website");
    }
}