using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Twilio.HttpClients.Abstract;
using Soenneker.Twilio.OpenApiClientUtil.Abstract;
using Soenneker.Twilio.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Twilio.OpenApiClientUtil;

///<inheritdoc cref="ITwilioOpenApiClientUtil"/>
public sealed class TwilioOpenApiClientUtil : ITwilioOpenApiClientUtil
{
    private readonly AsyncSingleton<TwilioOpenApiClient> _client;

    public TwilioOpenApiClientUtil(ITwilioOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<TwilioOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Twilio:ApiKey");
            string authHeaderValueTemplate = configuration["Twilio:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new TwilioOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TwilioOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
