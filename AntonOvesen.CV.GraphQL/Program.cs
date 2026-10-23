using AntonOvesen.CV.GraphQL.Schemas;
using AntonOvesen.CV.GraphQL.Services;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson()
        .AddSchema<CVSchema>();
});

builder.Services.AddScoped<ICompanyService, CompanyService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
app.UseGraphQLGraphiQL(
    "/",
    new GraphiQLOptions
    {
        GraphQLEndPoint = "/graphql",
        SubscriptionsEndPoint = "/graphql"
    });

app.Run();