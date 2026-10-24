using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id).Description("Unique identifier");
            Field(x => x.FirstName).Description("Given name");
            Field(x => x.LastName).Description("Surname, or remaining names");
            Field(x => x.Email).Description("Email contact information");
            Field(x => x.Phone, typeof(PhoneType))
                .Description("Phone contact information")
                .Resolve(ctx => ctx.Source.Phone);

            #region Relations

            Field(x => x.Educations, typeof(ListGraphType<EducationType>))
                .Description("Educations this user attendend")
                .ResolveAsync(ctx =>
                {
                    var loader = ctx.RequestServices!.GetRequiredService<UserEducationDataLoader>();
                    return loader.LoadAsync(ctx.Source.Id);
                });
            Field(x => x.Companies, typeof(ListGraphType<CompanyType>))
                .Description("Companies this user worked for")
                .ResolveAsync(ctx =>
                {
                    var loader = ctx.RequestServices!.GetRequiredService<UserCompanyDataLoader>();
                    return loader.LoadAsync(ctx.Source.Id);
                });
            Field(x => x.Skills, typeof(ListGraphType<SkillType>))
                .Description("Skills this user has aquired")
                .ResolveAsync(ctx =>
                {
                    var loader = ctx.RequestServices!.GetRequiredService<UserSkillsDataLoader>();
                    return loader.LoadAsync(ctx.Source.Id);
                });
            Field(x => x.Projects, typeof(ListGraphType<ProjectType>))
                .Description("Projects this user has created or worked on")
                .ResolveAsync(ctx =>
                {
                    var loader = ctx.RequestServices!.GetRequiredService<UserProjectDataLoader>();
                    return loader.LoadAsync(ctx.Source.Id);
                });

            #endregion Relations
        }
    }

    public class UserInputType : InputObjectGraphType<User>
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field(x => x.FirstName).Description("Given name");
            Field(x => x.LastName).Description("Surname, or remaining names");
            Field(x => x.Email).Description("Email contact information");
            Field(x => x.Phone, typeof(PhoneInputType)).Description("Phone contact information");
        }
    }

    public class PhoneType : ObjectGraphType<Phone>
    {
        public PhoneType()
        {
            Field(x => x.Number).Description("Phone number without country code");
            Field(x => x.CountryCode).Description("International country code. E.g. \"+45\"");
        }
    }

    public class PhoneInputType : InputObjectGraphType<Phone>
    {
        public PhoneInputType()
        {
            Name = "PhoneInput";
            Field(x => x.Number).Description("Phone number without country code");
            Field(x => x.CountryCode).Description("International country code. E.g. \"+45\"");
        }
    }
}