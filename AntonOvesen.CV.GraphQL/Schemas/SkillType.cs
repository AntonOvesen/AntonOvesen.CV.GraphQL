using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class SkillType : ObjectGraphType<Skill>
{
    public SkillType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Title).Description("Short name of the skill");
        Field(x => x.Description).Description("Longer explination or description of the skill");
        //Field(x => x.User).Description("Who has this skill");
        //Field(x => x.Projects).Description("Projects this skill was used for");
        //Field(x => x.Educations).Description("Educations where this skill was honed");
        //Field(x => x.Companies).Description("Companies wher this skill was honed or applied");
    }
}
