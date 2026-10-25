using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.Repository;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas.Mutation;

public class UserMutations : ObjectGraphType
{
    public UserMutations()
    {
        Field<UserType>("create")
            .Argument<NonNullGraphType<UserInputType>>("user")
            .ResolveAsync(async ctx =>
            {
                var userRepository = ctx.RequestServices!.GetRequiredService<IUserRepository>();
                return await userRepository.Create(ctx.GetArgument<User>("user"), ctx.CancellationToken);
            });

        Field<UserType>("delete")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                var userRepository = ctx.RequestServices!.GetRequiredService<IUserRepository>();
                return await userRepository.Delete(ctx.GetArgument<Guid>("id"), ctx.CancellationToken);
            });
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

public class PhoneInputType : InputObjectGraphType<Phone>
{
    public PhoneInputType()
    {
        Name = "PhoneInput";
        Field(x => x.Number).Description("Phone number without country code");
        Field(x => x.CountryCode).Description("International country code. E.g. \"+45\"");
    }
}