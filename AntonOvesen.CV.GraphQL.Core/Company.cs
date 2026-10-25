namespace AntonOvesen.CV.GraphQL.Core;

public class Company
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Website { get; set; }
    public string? Role { get; set; }

    public DateTime From { get; set; }
    public DateTime? To { get; set; }

    #region Relations

    public Guid UserId { get; set; }
    public User? User { get; set; }

    #endregion Relations
}