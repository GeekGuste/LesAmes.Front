using LesAmes.Application.Services.Souls.ImpactFamilies;
using LesAmes.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace LesAmes.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<CreateImpactFamilyService>();
        services.AddScoped<UpdateImpactFamilyService>();
        services.AddScoped<GetImpactFamiliesService>();

        return services;
    }
}
