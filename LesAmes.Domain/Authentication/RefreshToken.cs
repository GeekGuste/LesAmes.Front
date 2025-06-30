namespace LesAmes.Domain.Authentication;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public bool IsRevoked { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
}
