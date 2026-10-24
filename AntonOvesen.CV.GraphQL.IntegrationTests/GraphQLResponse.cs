using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.IntegrationTests;

public class GraphQLResponse<TData>
{
    public TData? Data { get; set; }
}

public class GraphQLUserResponse 
{
    public User? User { get; set; }
}