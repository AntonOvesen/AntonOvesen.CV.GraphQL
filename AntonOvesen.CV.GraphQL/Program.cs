using AntonOvesen.CV.GraphQL.Infrastructure.Extensions;
using AntonOvesen.CV.GraphQL.Schemas;
using GraphQL.Server.Ui.GraphiQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson()
        .AddSchema<CVSchema>()
        .AddDataLoader();
});

builder.Services.AddMySqlContext(builder.Configuration);
builder.Services.AddDataLoaders();

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