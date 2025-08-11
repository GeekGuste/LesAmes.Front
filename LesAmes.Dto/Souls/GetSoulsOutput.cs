namespace LesAmes.Dto.Souls;

public class GetSoulsOutput
{
    public IEnumerable<SoulElementDto> Souls { get; set; } = new List<SoulElementDto>();
}
