using Soenneker.Jira.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Jira.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class JiraOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IJiraOpenApiHttpClient _httpclient;

    public JiraOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IJiraOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
