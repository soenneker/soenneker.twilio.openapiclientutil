[![](https://img.shields.io/nuget/v/soenneker.twilio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.twilio.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.twilio.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.twilio.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.twilio.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.twilio.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.twilio.openapiclientutil/codeql.yml?style=for-the-badge&label=CodeQL)](https://github.com/soenneker/soenneker.twilio.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Twilio.OpenApiClientUtil

Provides an authenticated, cached `TwilioOpenApiClient` using a Twilio API key from configuration.

## Installation

```bash
dotnet add package Soenneker.Twilio.OpenApiClientUtil
```

## Configuration

```json
{
  "Twilio": {
    "ApiKey": "SK...",
    "ApiSecret": "..."
  }
}
```

The API key SID and secret are sent with HTTP Basic authentication. Do not use an Account SID in place of `ApiKey`.

## Registration

```csharp
using Soenneker.Twilio.OpenApiClientUtil.Registrars;

services.AddTwilioOpenApiClientUtilAsScoped();
```

Use `AddTwilioOpenApiClientUtilAsSingleton()` when the provider itself should be shared for the application's lifetime. Both registrations use the singleton Twilio HTTP provider. Disposing a scoped client provider does not remove or dispose that shared HTTP client.

## Usage

```csharp
using Soenneker.Twilio.OpenApiClient;
using Soenneker.Twilio.OpenApiClientUtil.Abstract;

public sealed class TwilioService
{
    private readonly ITwilioOpenApiClientUtil _clients;

    public TwilioService(ITwilioOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<TwilioOpenApiClient> GetClient(
        CancellationToken cancellationToken)
    {
        return await _clients.Get(cancellationToken);
    }
}
```

`Get()` creates the authenticated client once for that provider instance and returns the same instance afterward. Client creation is thread-safe. API request failures are still surfaced by the generated Kiota client.
