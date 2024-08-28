using System.Text;

namespace VisaApiClient
{
    public class ClientBase
    {
        private const string LoginPath = "users/login";

        protected string? AuthToken { get; private set; }

        protected void SetAuthToken(string token)
        {
            AuthToken = token;
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
            return Task.FromResult(msg);
        }


        protected async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
            => await Task.CompletedTask;

        protected async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
            => await Task.CompletedTask;

        protected async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.RequestMessage!.RequestUri!.AbsolutePath == LoginPath)
            {
                var token = await response.Content.ReadAsStringAsync(cancellationToken);
                SetAuthToken(token);
            }
        }
    }
}
