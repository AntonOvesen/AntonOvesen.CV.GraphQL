namespace AntonOvesen.CV.GraphQL.Core;

public class Company
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime Founded { get; set; }
    public string? Website { get; set; }

    public DateTime From { get; set; }
    public DateTime? To { get; set; }

    #region Relations

    public User? User { get; set; }

    public List<Skill>? Skills { get; set; }

    #endregion Relations
}