using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Jira.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Jira.HttpClients;

public sealed class JiraOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration configuration) : IJiraOpenApiHttpClient
{
    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        string? configuredUrl = configuration["Jira:ClientBaseUrl"];
        if (!Uri.TryCreate(configuredUrl, UriKind.Absolute, out Uri? baseAddress) ||
            baseAddress.Scheme != Uri.UriSchemeHttps || !string.IsNullOrEmpty(baseAddress.UserInfo) ||
            !string.IsNullOrEmpty(baseAddress.Query) || !string.IsNullOrEmpty(baseAddress.Fragment))
            throw new InvalidOperationException("Jira:ClientBaseUrl must be an absolute HTTPS API base URL without credentials, query, or fragment.");

        return httpClientCache.Get(nameof(JiraOpenApiHttpClient), baseAddress, static address => new HttpClientOptions
        {
            BaseAddress = new Uri(address.AbsoluteUri.TrimEnd('/') + "/"),
            DefaultRequestHeaders = new Dictionary<string, string> { ["Accept"] = "application/json" }
        }, cancellationToken);
    }

    public void Dispose() => httpClientCache.RemoveSync(nameof(JiraOpenApiHttpClient));

    public ValueTask DisposeAsync() => httpClientCache.Remove(nameof(JiraOpenApiHttpClient));
}
