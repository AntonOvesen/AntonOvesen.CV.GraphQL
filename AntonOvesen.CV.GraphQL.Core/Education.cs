namespace AntonOvesen.CV.GraphQL.Core;

public class Education
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Level { get; set; } // Todo: enum?

    #region Relations

    public Institution? Institution { get; set; }

    public List<Skill>? Skills { get; set; }

    #endregion Relations
}

public class Institution
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public string? Website { get; set; }
}