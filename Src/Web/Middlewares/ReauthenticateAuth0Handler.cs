using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Clients;
using Microsoft.Extensions.Logging;
using Models;
using Models.Requests;

namespace Web.Middlewares
{
    public class ReauthenticateAuth0Handler : DelegatingHandler
    {
        private readonly ILogger<ReauthenticateAuth0Handler> _logger;
        private readonly TokenRequest _tokenRequest;
        private readonly AccessToken _accessToken;
        private readonly IAccessTokenClient _client;

        public ReauthenticateAuth0Handler(
            ILogger<ReauthenticateAuth0Handler> logger, TokenRequest tokenRequest, AccessToken accessToken, IAccessTokenClient accessTokenClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
            _accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            _client = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogInformation("Request was unauthorized: requesting new token...");

                var accessToken = (await _client.Token(_tokenRequest)).AccessToken;
                _logger.LogInformation($"Received access token");

                var auth = $"Bearer {accessToken}";
                _accessToken.Value = auth;
                request.Headers.Authorization = AuthenticationHeaderValue.Parse(auth);
            }
            else
            {
                _logger.LogInformation("Authentication OK, returning response");

                return response;
            }

            _logger.LogInformation("Resending request with correct authorization header");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
