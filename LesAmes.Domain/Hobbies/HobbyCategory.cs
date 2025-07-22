namespace LesAmes.Domain.Hobbies;

public class HobbyCategory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }

    public ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
}