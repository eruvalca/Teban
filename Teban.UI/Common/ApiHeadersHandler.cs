using System.Net.Http.Headers;

namespace Teban.UI.Common;
public class ApiHeadersHandler : DelegatingHandler
{
    private readonly ILocalSecureStorage _localStorage;

    public ApiHeadersHandler(ILocalSecureStorage localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var auth = request.Headers.Authorization;

        if (auth != null)
        {
            string token = await _localStorage.GetAsync("authToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
