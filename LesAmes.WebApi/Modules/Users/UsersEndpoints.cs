using LesAmes.Application.Services.Users;
using LesAmes.Domain.Authentication;
using LesAmes.Dto.Users;
using Microsoft.AspNetCore.Authorization;

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

        app.MapPut("/me",
        [Authorize(Policy = "User")]
        async (UserService userService, UpdateUserDto dto) =>
        {
            var result = await userService.UpdateCurrentUserAsync(dto);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPut("/me/update-password",
        [Authorize(Policy = "User")]
        async (UserService userService, ChangePasswordDto dto) =>
        {
            var result = await userService.ChangePasswordAsync(dto);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPut("/users/{id}",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id, UpdateUserDto dto) =>
        {
            var result = await userService.UpdateUserAsync(id, dto);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPut("/users/{id}/password",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id, ChangePasswordDto dto) =>
        {
            var result = await userService.ChangePasswordAsync(dto);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPost("/users/{id}/disable",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id) =>
        {
            var result = await userService.DisableUserAsync(id);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPost("/users/{id}/enable",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id) =>
        {
            var result = await userService.EnableUserAsync(id);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapPost("/users/{id}/roles",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id, RoleDto dto) =>
        {
            var result = await userService.AddRoleAsync(id, dto.Role);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        app.MapDelete("/users/{id}/roles/{role}",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (UserService userService, string id, string role) =>
        {
            var result = await userService.RemoveRoleAsync(id, role);
            return result.Succeeded ? Results.NoContent() : Results.BadRequest(result.Errors);
        });

        return app;
    }
}
