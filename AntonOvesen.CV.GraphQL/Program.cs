using AntonOvesen.CV.GraphQL;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddGraphQL(builder =>
{
    builder.AddSchema<WhatSchema>().AddSystemTextJson();
});

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
public class WhatSchema : Schema
{
    public WhatSchema()
    {
        Query = new CompanyQuery();
    }

}