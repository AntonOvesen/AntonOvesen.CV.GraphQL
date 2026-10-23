namespace AntonOvesen.CV.GraphQL.Core;

public class Project
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Ref { get; set; }

    #region Relations

    public List<User>? Users { get; set; }

    // Note this doesnt really make sense.
    // Skill needs to be more clearly defined or tied to a user maybe?
    // Or one-way relation between skill and their origin maybe.. :think:
    public List<Skill>? Skills { get; set; }

    #endregion Relations
}