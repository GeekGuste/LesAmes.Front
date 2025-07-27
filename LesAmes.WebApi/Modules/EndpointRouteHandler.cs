using LesAmes.WebApi.Modules.Souls;
using LesAmes.WebApi.Modules.Users;

namespace LesAmes.WebApi.Modules;

public static class EndpointRouteHandler
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/users").RegisterUsersEndpoints().WithTags("User");
        app.MapGroup("/impact-families").RegisterImpactFamiliesEndpoints().WithTags("ImpactFamilies");
        app.MapGroup("/age-ranges").RegisterAgeRangesEndpoints().WithTags("AgeRanges");
        app.MapGroup("/hobbies").RegisterHobbiesEndpoints().WithTags("Hobbies");

        return app;
    }
}
