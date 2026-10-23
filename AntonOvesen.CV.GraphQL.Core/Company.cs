namespace AntonOvesen.CV.GraphQL.Core;

public class Company
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime Founded { get; set; }
    public string? Website { get; set; }
}