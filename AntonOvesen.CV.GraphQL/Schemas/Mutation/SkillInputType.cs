using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class SkillInputType : InputObjectGraphType<Skill>
{
    public SkillInputType()
    {
        Name = "SkillInput";
        Field(x => x.Title).Description("Short name of the skill");
        Field(x => x.Description).Description("Longer explination or description of the skill");
        Field(x => x.UserId).Description("Who has this skill");
    }
}