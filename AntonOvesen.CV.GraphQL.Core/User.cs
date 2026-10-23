namespace AntonOvesen.CV.GraphQL.Core;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    #region Relations

    public List<Education>? Educations { get; set; }
    public List<Company>? Companies { get; set; }
    public List<Skill>? Skills { get; set; }
    public List<Project>? Projects { get; set; }

    #endregion Relations
}