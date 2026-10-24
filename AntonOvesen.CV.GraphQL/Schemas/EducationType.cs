using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class EducationType : ObjectGraphType<Education>
{
    public EducationType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Name).Description("Name or type of education");
        Field(x => x.Level).Description("Education level. e.g. \"Erhvervsakademiuddannelse\", \"Kandidat\" etc");
        Field(x => x.Institution, typeof(InstitutionType))
            .Resolve(ctx => ctx.Source.Institution)
            .Description("Institution this education was completed at");
        Field(x => x.User, typeof(UserType))
            .Resolve(ctx => ctx.Source.User)
            .Description("User who completed this education");
        Field(x => x.Skills, typeof(ListGraphType<SkillType>))
            .Resolve(ctx => ctx.Source.Skills)
            .Description("Skills the user aquired when completing this education");
    }
}

public class InstitutionType : ObjectGraphType<Institution>
{
    public InstitutionType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Name).Description("Name of the institution");
        Field(x => x.Website).Description("Link to the institutions website");
        Field(x => x.Educations, typeof(ListGraphType<EducationType>)).Description("Educations that were completed at this institution");
    }
}