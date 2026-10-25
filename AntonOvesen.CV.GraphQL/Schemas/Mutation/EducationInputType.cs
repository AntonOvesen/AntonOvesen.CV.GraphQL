using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class EducationInputType : InputObjectGraphType<Education>
{
    public EducationInputType()
    {
        Name = "EducationInput";
        Field(x => x.Name).Description("Name or type of education");
        Field(x => x.Level).Description("Education level. e.g. \"Erhvervsakademiuddannelse\", \"Kandidat\" etc");
        Field(x => x.InstitutionId).Description("Institution this education was completed at");
        Field(x => x.UserId).Description("User who completed this education");
    }
}
