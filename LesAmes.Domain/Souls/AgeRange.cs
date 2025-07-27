namespace LesAmes.Domain.Souls;

public class AgeRange
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int AgeMin { get; set; }
    public int AgeMax { get; set; }
    public bool IsActive { get; set; } = true;
}
