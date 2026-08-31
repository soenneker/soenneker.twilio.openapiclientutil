using Soenneker.Twilio.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Twilio.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides an authenticated, cached Twilio OpenAPI client.
/// </summary>
public interface ITwilioOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the authenticated client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel client creation.</param>
    /// <returns>The cached client.</returns>
    ValueTask<TwilioOpenApiClient> Get(CancellationToken cancellationToken = default);
}
