using Soenneker.Twilio.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Twilio.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class TwilioOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly ITwilioOpenApiClientUtil _openapiclientutil;

    public TwilioOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<ITwilioOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
