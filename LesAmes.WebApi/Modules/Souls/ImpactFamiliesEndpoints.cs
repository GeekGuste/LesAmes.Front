using LesAmes.Application.Services.Souls.ImpactFamilies;
using LesAmes.Domain.Authentication;
using LesAmes.Dto.Souls;
using Microsoft.AspNetCore.Authorization;

namespace LesAmes.WebApi.Modules.Souls;

public static class ImpactFamiliesEndpoints
{
    public static RouteGroupBuilder RegisterImpactFamiliesEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (CreateImpactFamilyService createImpactFamilyService, CreateImpactFamilyInput input) => createImpactFamilyService.CreateImpactFamilyAsync(input))
            .WithDisplayName("RegisterImpactFamily");

        app.MapPut("/{id}",
        [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (int id, UpdateImpactFamilyInput input, UpdateImpactFamilyService updateImpactFamilyService) => updateImpactFamilyService.UpdateAsync(id, input))
            .WithDisplayName("UpdateImpactFamily");

        app.MapGet("",
        [Authorize()]
        async (GetImpactFamiliesService getImpactFamiliesService) => getImpactFamiliesService.GetAllImpactFamiliesAsync())
            .WithDisplayName("GetImpactFamily");

        return app;
    }
}
