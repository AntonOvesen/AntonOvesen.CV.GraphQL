using AntonOvesen.CV.GraphQL.Core;

namespace AntonOvesen.CV.GraphQL.Services;

public interface IEducationService
{
    Task<List<Education>> GetEducations(CancellationToken cancellationToken);
}
public class EducationService : IEducationService
{
    public async Task<List<Education>> GetEducations(CancellationToken cancellationToken)
    {
        return [
            new() {
                Id = Guid.NewGuid(),
                Institution = new Institution {
                    Id = Guid.NewGuid(),
                    Name = "EA Dania",
                    Website = "https://eadania.dk/"
                },
                Level = "Erhvervsakademisk uddannelse",
                Name = "Computer Science (AP)",
                User = new() {
                    FirstName = "John"
                }
            }
        ];
    }
}
