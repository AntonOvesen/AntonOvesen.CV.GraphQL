using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class ProjectInputType : InputObjectGraphType<Project>
{
    public ProjectInputType()
    {
        Field(x => x.Title).Description("Title or name of the project");
        Field(x => x.Description).Description("Describes the project");
        Field(x => x.Ref).Description("Link or other reference to the project");
        Field(x => x.UserId).Description("Whom his project belongs to");
    }
}