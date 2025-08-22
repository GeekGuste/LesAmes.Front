namespace LesAmes.Dto.Souls;

public class SoulDto
{
    public SexeDto Sexe { get; set; }
    public string? TutorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Telephone { get; set; }
    public string Quartier { get; set; }
    public string Email { get; set; }
    public string AgeRangeId { get; set; }
    public DateTime DateCreation { get; set; }
    public List<int> HobbiesIds { get; set; }
}
