using LesAmes.WebApi.Modules.Souls;
using LesAmes.WebApi.Modules.Users;

namespace LesAmes.WebApi.Modules;

public static class EndpointRouteHandler
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/users").RegisterUsersEndpoints().WithTags("User");
        app.MapGroup("/impact-families").RegisterImpactFamiliesEndpoints().WithTags("ImpactFamily");

        return app;
    }
}
