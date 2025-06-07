namespace LesAmes.Domain.Port;

public interface IUserService
{
    Task<bool> RegisterAsync(string email, string password);
    Task<string> LoginAsync(string email, string password);
}
