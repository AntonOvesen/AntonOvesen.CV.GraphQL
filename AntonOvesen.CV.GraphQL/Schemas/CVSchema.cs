using AntonOvesen.CV.GraphQL.Schemas.Mutation;
using AntonOvesen.CV.GraphQL.Schemas.Query;
using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL.Schemas;

public class CVSchema : Schema
{
    public CVSchema()
    {
        Query = new CVSchemaQuery();
        Mutation = new CVSchemaMutation();
    }
}