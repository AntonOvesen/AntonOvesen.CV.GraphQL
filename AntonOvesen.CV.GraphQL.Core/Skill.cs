namespace AntonOvesen.CV.GraphQL.Core;

public class Skill
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    #region Relations

    public User? User { get; set; }
    public List<Project>? Projects { get; set; }
    public List<Education>? Educations { get; set; }
    public List<Company>? Companies { get; set; }

    #endregion Relations
}