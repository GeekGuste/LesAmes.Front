using LesAmes.Domain.Port;

namespace LesAmes.Application.Services;

public class UserService
{
    private readonly IUserService _userService;

    public UserService(IUserService userService)
    {
        _userService = userService;
    }

    public Task<bool> RegisterUserAsync(string email, string password)
    {
        return _userService.RegisterAsync(email, password);
    }

    public Task<string?> LoginUserAsync(string email, string password)
    {
        return _userService.LoginAsync(email, password);
    }
}
