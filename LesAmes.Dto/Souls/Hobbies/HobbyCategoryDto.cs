namespace LesAmes.Dto.Souls.Hobbies;

public class HobbyCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<HobbyDto> Hobbies { get; set; }
}
