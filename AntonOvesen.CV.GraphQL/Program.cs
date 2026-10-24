using AntonOvesen.CV.GraphQL.Infrastructure;
using AntonOvesen.CV.GraphQL.Infrastructure.Extensions;
using AntonOvesen.CV.GraphQL.Schemas;
using AntonOvesen.CV.GraphQL.Services;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder =>
{
    builder.AddSystemTextJson()
        .AddSchema<CVSchema>();
});

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEducationService, EducationService>();

builder.Services.AddMySqlContext(builder.Configuration);

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