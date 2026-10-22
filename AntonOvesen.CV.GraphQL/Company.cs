using GraphQL.Types;

namespace AntonOvesen.CV.GraphQL;

public class Company
{
    public string? Name { get; set; }
    public DateTime Founded { get; set; }
    public string? Website { get; set; }
}

public class CompanyType : ObjectGraphType<Company>
{
    public CompanyType()
    {
        Field(x => x.Name).Description("The current name of the company");
        Field(x => x.Founded).Description("When was the company founded");
        Field(x => x.Website).Description("Link to the companies website");
    }
}

public class CompanyQuery : ObjectGraphType
{
    public CompanyQuery()
    {
        Field<CompanyType>("company").Resolve(context => new Company
        {
            Name = "Tryg CardsAffinity",
            Founded = DateTime.Parse("2002-01-01"),
            Website = "tryg.dk"
        });
    }
}