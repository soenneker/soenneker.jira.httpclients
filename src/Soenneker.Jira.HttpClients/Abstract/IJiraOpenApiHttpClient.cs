using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Jira.HttpClients.Abstract;

/// <summary>
/// A cached HTTP transport for Jira. Authentication is attached by the OpenAPI client utility.
/// </summary>
public interface IJiraOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the shared HTTP transport using Jira:ClientBaseUrl. Configure the base URL before first use.</summary>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
