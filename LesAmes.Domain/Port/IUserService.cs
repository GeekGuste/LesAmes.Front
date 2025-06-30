using LesAmes.Domain.Authentication;

namespace LesAmes.Domain.Port;

public interface IUserService
{
    Task<bool> RegisterAsync(string email, string password);
    Task<TokenResponse> LoginAsync(string email, string password);
}
