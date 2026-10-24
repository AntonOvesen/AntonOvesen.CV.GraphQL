using AntonOvesen.CV.GraphQL.Core;
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
            //Field(x => x.Phone).Description("Phone contact information");
            //Field(x => x.Educations).Description("Educations this user attendend");
            //Field(x => x.Companies).Description("Companies this user worked for");
            //Field(x => x.Skills).Description("Skills this user has aquired");
            //Field(x => x.Projects).Description("Projects this user has created or worked on");
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
}
