namespace AntonOvesen.CV.GraphQL.Core;

public class Project
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Ref { get; set; }

    #region Relations

    public User? User { get; set; }
    public List<Skill>? Skills { get; set; }

    #endregion Relations
}