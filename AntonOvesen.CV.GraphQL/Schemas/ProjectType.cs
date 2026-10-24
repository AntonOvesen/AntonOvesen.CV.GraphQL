using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class ProjectType : ObjectGraphType<Project>
{
    public ProjectType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Title).Description("Title or name of the project");
        Field(x => x.Description).Description("Describes the project");
        Field(x => x.Ref).Description("Link or other reference to the project");
        Field(x => x.User, typeof(UserType)).Description("Whom his project belongs to");
        Field(x => x.Skills, typeof(ListGraphType<SkillType>)).Description("Skills the user aquired or used for this project");
    }
}
