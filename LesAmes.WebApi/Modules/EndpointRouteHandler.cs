using LesAmes.WebApi.Modules.Users;

namespace LesAmes.WebApi.Modules;

public static class EndpointRouteHandler
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/users").RegisterUsersEndpoints().WithTags("User");

        return app;
    }
}
