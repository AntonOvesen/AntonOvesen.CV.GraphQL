namespace AntonOvesen.CV.GraphQL.Core;

public class Skill
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    #region Relations

    public Guid UserId { get; set; }
    public User? User { get; set; }

    #endregion Relations
}