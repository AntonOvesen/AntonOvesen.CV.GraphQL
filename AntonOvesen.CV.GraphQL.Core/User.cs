namespace AntonOvesen.CV.GraphQL.Core;

public class User
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Phone? Phone { get; set; }

    #region Relations

    public List<Education>? Educations { get; set; }
    public List<Company>? Companies { get; set; }
    public List<Skill>? Skills { get; set; }
    public List<Project>? Projects { get; set; }

    #endregion Relations
}

public class Phone
{
    public required string Number { get; set; }
    public required string CountryCode { get; set; }
}