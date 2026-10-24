using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class SkillType : ObjectGraphType<Skill>
{
    public SkillType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Title).Description("Short name of the skill");
        Field(x => x.Description).Description("Longer explination or description of the skill");

        #region Relations

        Field(x => x.User, typeof(UserType))
            .Description("Who has this skill")
            .ResolveAsync(ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserDataLoader>();
                return loader.LoadAsync(ctx.Source.UserId);
            });

        #endregion Relations
    }
}