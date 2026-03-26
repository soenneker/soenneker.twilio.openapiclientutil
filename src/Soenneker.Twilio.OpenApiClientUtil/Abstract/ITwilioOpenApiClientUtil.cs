using Soenneker.Twilio.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Twilio.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ITwilioOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<TwilioOpenApiClient> Get(CancellationToken cancellationToken = default);
}
