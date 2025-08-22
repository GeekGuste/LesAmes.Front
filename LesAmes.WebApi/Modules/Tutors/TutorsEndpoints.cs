using LesAmes.Application.Services.Tutors;
using LesAmes.Domain.Authentication;
using LesAmes.Dto.Tutors;
using Microsoft.AspNetCore.Authorization;

namespace LesAmes.WebApi.Modules.Tutors;

public static class TutorsEndpoints
{
    public static RouteGroupBuilder RegisterTutorsEndpoints(this RouteGroupBuilder app)
    {
        app.MapPost("/register",
        async (RegisterUpdateTutorService registerUpdateTutorService, TutorDto input) => registerUpdateTutorService.RegisterAsync(input))
        .WithDisplayName("RegisterNewTutor");

        app.MapPost("/update/me",
        [Authorize(Roles = Roles.Tutor)]
        async (RegisterUpdateTutorService registerUpdateTutorService, TutorDto input) => registerUpdateTutorService.UpdateCurrentUserAsync(input))
        .WithDisplayName("UpdateCurrentTutor");

        return app;
    }
}
