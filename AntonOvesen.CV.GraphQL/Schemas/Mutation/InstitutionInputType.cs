using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class InstitutionInputType : InputObjectGraphType<Institution>
{
    public InstitutionInputType()
    {
        Name = "InstitutionInput";
        Field(x => x.Name).Description("Name of the institution");
        Field(x => x.Website).Description("Link to the institutions website");
    }
}
