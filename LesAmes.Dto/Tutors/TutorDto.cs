namespace LesAmes.Dto.Tutors;

public class TutorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int BirthYear { get; set; }

    public List<string> HobbiesIds { get; set; }
}
