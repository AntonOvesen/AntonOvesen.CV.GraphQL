using AntonOvesen.CV.GraphQL.Core;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

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

public class PhoneInputType : InputObjectGraphType<Phone>
{
    public PhoneInputType()
    {
        Name = "PhoneInput";
        Field(x => x.Number).Description("Phone number without country code");
        Field(x => x.CountryCode).Description("International country code. E.g. \"+45\"");
    }
}