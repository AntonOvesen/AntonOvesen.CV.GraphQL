using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class SkillMutations : ObjectGraphType
{
    public SkillMutations()
    {
        Field<SkillType>("create")
            .Argument<NonNullGraphType<SkillInputType>>("skill")
            .ResolveAsync(async ctx =>
            {
                var skillRepository = ctx.RequestServices!.GetRequiredService<ISkillRepository>();
                return await skillRepository.Create(ctx.GetArgument<Skill>("skill"), ctx.CancellationToken);
            });

        Field<SkillType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var skillRepository = ctx.RequestServices!.GetRequiredService<ISkillRepository>();
                return await skillRepository.Delete(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
    }
}

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