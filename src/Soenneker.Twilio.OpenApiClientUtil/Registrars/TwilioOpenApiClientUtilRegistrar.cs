using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Twilio.HttpClients.Registrars;
using Soenneker.Twilio.OpenApiClientUtil.Abstract;

namespace Soenneker.Twilio.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class TwilioOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="TwilioOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTwilioOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTwilioOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ITwilioOpenApiClientUtil, TwilioOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="TwilioOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTwilioOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddTwilioOpenApiHttpClientAsSingleton()
                .TryAddScoped<ITwilioOpenApiClientUtil, TwilioOpenApiClientUtil>();

        return services;
    }
}
