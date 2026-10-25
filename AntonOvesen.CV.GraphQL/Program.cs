using AntonOvesen.CV.GraphQL;
using AntonOvesen.CV.GraphQL.Infrastructure.Extensions;
using AntonOvesen.CV.GraphQL.Schemas;
using GraphQL.Server.Ui.GraphiQL;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson()
        .AddSchema<CVSchema>()
        .AddDataLoader();
});

builder.Services.AddMySqlContext(builder.Configuration);
builder.Services.AddDataLoaders();
builder.Services.AddRepositories();
builder.Services.AddHostedService<Migrator>();

var app = builder.Build();

app.MapDefaultEndpoints();

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