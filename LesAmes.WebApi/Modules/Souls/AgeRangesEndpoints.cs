using LesAmes.Application.Services.Souls.AgeRanges;
using LesAmes.Domain.Authentication;
using LesAmes.Dto.Souls.AgeRanges;
using Microsoft.AspNetCore.Authorization;

namespace LesAmes.WebApi.Modules.Souls;

public static class AgeRangesEndpoints
{
    public static RouteGroupBuilder RegisterAgeRangesEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("",
                [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (CreateAgeRangeService createAgeRangeService, AgeRangeInput input) => createAgeRangeService.CreateAgeRangeAsync(input))
            .WithDisplayName("CreateAgeRange");

        app.MapPut("/{id}",
                [Authorize(Roles = $"{Roles.Admin},{Roles.SuperAdmin}")]
        async (string id, AgeRangeInput input, UpdateAgeRangeService updateAgeRangeService) => updateAgeRangeService.UpdateAgeRangeAsync(id, input))
            .WithDisplayName("UpdateAgeRange");

        app.MapGet("",
        [Authorize()]
        async (GetAgeRangeService getAgeRangeService) => getAgeRangeService.GetAgeRangeAsync())
            .WithDisplayName("GetAgeRange");

        return app;
    }
}
