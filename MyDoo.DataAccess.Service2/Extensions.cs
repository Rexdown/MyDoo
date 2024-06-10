using System.Diagnostics.CodeAnalysis;

namespace MyDoo.DataAccess.Service2;

public static class Extensions
{
    public static IServiceCollection AddScopedSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
        this IServiceCollection services
    )
        where TService : class
        where TImplementation : class, TService {
        if (services is null) {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped<TImplementation>();

        return services.AddSingleton<TService>(sp => sp.CreateScope().ServiceProvider.GetRequiredService<TImplementation>());
    }
}