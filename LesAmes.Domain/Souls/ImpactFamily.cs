namespace LesAmes.Domain.Souls;

public class ImpactFamily
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Address { get; set; }
    public string Quartiers { get; set; }
    public string PilotName { get; set; }
    public string PilotContact { get; set; }
    public bool IsActive { get; set; } = true;
}
