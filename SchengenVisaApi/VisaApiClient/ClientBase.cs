using System.Text;

namespace VisaApiClient
{
    public class ClientBase
    {
        protected AuthToken? AuthToken { get; private set; }

        public void SetAuthToken(AuthToken token)
        {
            AuthToken = token;
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();

            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken?.Token);

            return Task.FromResult(msg);
        }


        protected async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
            => await Task.CompletedTask;

        protected async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        protected async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
            => await Task.CompletedTask;
    }
}
