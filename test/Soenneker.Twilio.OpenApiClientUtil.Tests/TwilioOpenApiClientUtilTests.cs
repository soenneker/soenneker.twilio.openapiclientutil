using Soenneker.Twilio.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Twilio.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TwilioOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ITwilioOpenApiClientUtil _openapiclientutil;

    public TwilioOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ITwilioOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
