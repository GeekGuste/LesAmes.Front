using LesAmes.Application.Services;

namespace LesAmes.WebApi.Modules.Users;

public static class UsersEndpoints
{
    public static RouteGroupBuilder RegisterUsersEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("/register", async (UserService userService, string email, string password) =>
        {
            var result = await userService.RegisterUserAsync(email, password);

            return result ? Results.Ok("User created") : Results.BadRequest("Registration failed");
        });

        app.MapPost("/login", async (UserService userService, string email, string password) =>
        {
            var token = await userService.LoginUserAsync(email, password);

            return token != null ? Results.Ok(token) : Results.Unauthorized();
        });

        return app;
    }
}
