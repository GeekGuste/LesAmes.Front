using LesAmes.Domain.Port;
using Microsoft.Extensions.DependencyInjection;

namespace LesAmes.Infrastructure.Database;

public static class DatabaseDependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserServiceIdentity>();

        return services;
    }
}
