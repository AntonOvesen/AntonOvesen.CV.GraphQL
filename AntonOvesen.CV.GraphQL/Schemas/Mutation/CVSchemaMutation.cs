using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class CVSchemaMutation : ObjectGraphType
{
    public CVSchemaMutation()
    {
        Field<UserMutations>("user");
        Field<EducationMutations>("education");
        Field<InstitutionMutations>("institution");
        Field<SkillMutations>("skill");
        Field<CompanyMutations>("company");
        Field<ProjectMutations>("project");
    }
}