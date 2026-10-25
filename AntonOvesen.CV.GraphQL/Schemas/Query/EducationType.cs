using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Query;

public class EducationType : ObjectGraphType<Education>
{
    public EducationType()
    {
        Field(x => x.Id).Description("Unique identifier");
        Field(x => x.Name).Description("Name or type of education");
        Field(x => x.Level).Description("Education level. e.g. \"Erhvervsakademiuddannelse\", \"Kandidat\" etc");

        #region Relations

        Field(x => x.Institution, typeof(InstitutionType))
            .Description("Institution this education was completed at")
            .ResolveAsync(ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<InstitutionDataLoader>();
                return loader.LoadAsync(ctx.Source.InstitutionId);
            });

        Field(x => x.User, typeof(UserType))
            .Description("User who completed this education")
            .ResolveAsync(ctx =>
            {
                var loader = ctx.RequestServices!.GetRequiredService<UserDataLoader>();
                return loader.LoadAsync(ctx.Source.UserId);
            });

        #endregion Relations
    }
}

