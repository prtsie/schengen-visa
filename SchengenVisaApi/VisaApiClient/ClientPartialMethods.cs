using System.Text;
// ReSharper disable UnusedParameter.Local

namespace VisaApiClient;

public partial class Client
{
    public AuthToken? AuthToken { get; set; }

    private Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
            var msg = new HttpRequestMessage();

            msg.Headers.Authorization = new("Bearer", AuthToken?.Token);

            return Task.FromResult(msg);
        }


    private async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        => await Task.CompletedTask;

    private async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
    {
            await Task.CompletedTask;
        }

    private async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        => await Task.CompletedTask;
}
