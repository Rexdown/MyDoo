using System.Diagnostics.CodeAnalysis;

namespace MyBot;

public static class BotExtensions
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

    public static IServiceCollection AddScopedSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> implementationFactory
    )
        where TService : class {
        if (services is null) {
            throw new ArgumentNullException(nameof(services));
        }

        if (implementationFactory is null) {
            throw new ArgumentNullException(nameof(implementationFactory));
        }

        return services.AddSingleton(sp => implementationFactory(sp.CreateScope().ServiceProvider));
    }

    public static IServiceCollection AddScopedSingleton<TService, TImplementation>(
        this IServiceCollection services,
        Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService {
        if (services is null) {
            throw new ArgumentNullException(nameof(services));
        }

        if (implementationFactory is null) {
            throw new ArgumentNullException(nameof(implementationFactory));
        }

        return services.AddSingleton<TService>(sp => implementationFactory(sp.CreateScope().ServiceProvider));
    }
}