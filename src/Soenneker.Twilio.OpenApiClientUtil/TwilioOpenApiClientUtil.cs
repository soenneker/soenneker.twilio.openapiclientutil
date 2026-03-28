using System;
using System.Net.Http;
using System.Text;
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

            (string authHeaderName, string authHeaderValue) = GetAuthenticationHeader(configuration);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue), httpClient: httpClient);

            return new TwilioOpenApiClient(requestAdapter);
        });
    }

    private static (string headerName, string headerValue) GetAuthenticationHeader(IConfiguration configuration)
    {
        var apiKey = configuration.GetValueStrict<string>("Twilio:ApiKey");
        var apiSecret = configuration.GetValueStrict<string>("Twilio:ApiSecret");
        string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{apiSecret}"));

        return ("Authorization", $"Basic {credentials}");
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
