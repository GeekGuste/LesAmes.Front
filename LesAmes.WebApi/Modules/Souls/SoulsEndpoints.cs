using LesAmes.Application.Services.Souls;
using LesAmes.Domain.Authentication;
using LesAmes.Dto.Souls;
using Microsoft.AspNetCore.Authorization;

namespace LesAmes.WebApi.Modules.Souls;

public static class SoulsEndpoints
{
    public static RouteGroupBuilder RegisterSoulsEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (RegisterSoulsService registerSoulsService, SoulDto input) => registerSoulsService.RegisterSoulAsync(input))
            .WithDisplayName("RegisterSoul");

        app.MapPut("/{id}",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (string id, SoulDto input, UpdateSoulsService updateSoulsService) => updateSoulsService.UpdateSoulAsync(id, input))
            .WithDisplayName("UpdateSoul");

        app.MapPatch("/{id}",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (string id, string tutorId, UpdateSoulsService updateSoulsService) => updateSoulsService.SetSoulTutor(id, tutorId))
            .WithDisplayName("SetSoulTutor");

        return app;
    }
}
