using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AlfaCardToCardBot.Helpers
{
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestStr = request.ToString();
            if (request.Content != null)
            {
                var contentStr = await request.Content.ReadAsStringAsync();
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            
            var responseStr = response.ToString();
            if (response.Content != null)
            {
                var responseContentStr = await response.Content.ReadAsStringAsync();
            }

            return response;
        }
    }
}