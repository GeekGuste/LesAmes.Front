using LesAmes.Application.Services.Souls.AgeRanges;
using LesAmes.Application.Services.Souls.Hobbies;
using LesAmes.Application.Services.Souls.HobbyCategories;
using LesAmes.Application.Services.Souls.ImpactFamilies;
using LesAmes.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LesAmes.Application.Services;

public static class DependencyInjectionService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<CreateImpactFamilyService>();
        services.AddScoped<UpdateImpactFamilyService>();
        services.AddScoped<GetImpactFamiliesService>();

        services.AddScoped<CreateAgeRangeService>();
        services.AddScoped<UpdateAgeRangeService>();
        services.AddScoped<GetAgeRangeService>();

        services.AddScoped<CreateHobbyCategoryService>();
        services.AddScoped<GetHobbyCategoriesService>();
        services.AddScoped<UpdateHobbyCategoryService>();

        return services;
    }
}
