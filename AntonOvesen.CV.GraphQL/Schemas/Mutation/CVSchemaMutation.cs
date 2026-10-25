using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class CVSchemaMutation : ObjectGraphType
{
    public CVSchemaMutation()
    {
        // .Resolve(_ => new { }); what is this hack? :kekw:
        Field<UserMutations>("user").Resolve(_ => new { });
        Field<EducationMutations>("education").Resolve(_ => new { });
        Field<InstitutionMutations>("institution").Resolve(_ => new { });
        Field<SkillMutations>("skill").Resolve(_ => new { });
        Field<CompanyMutations>("company").Resolve(_ => new { });
        Field<ProjectMutations>("project").Resolve(_ => new { });
    }
}