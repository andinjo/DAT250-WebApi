using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Clients;
using Microsoft.Extensions.Logging;
using Models;
using Models.Core;
using Refit;

namespace Services.ClientWrappers
{
    public class UserClientWrapper : IUserClientWrapper
    {
        private readonly IUserClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<UserClientWrapper> _logger;
        private readonly string _accessToken;

        public UserClientWrapper(
            IUserClient client,
            IMapper mapper,
            ILogger<UserClientWrapper> logger,
            AccessToken accessToken)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accessToken = accessToken?.Value ?? throw new ArgumentNullException(nameof(accessToken));
        }

        public async Task<User> Get(string userId)
        {
            try
            {
                _logger.LogInformation($"Fetching request for user {userId}");
                var response = await _client.Get(userId, _accessToken);
                var user = _mapper.Map<User>(response);

                _logger.LogInformation($"Found user {user.Username}");
                return user;
            }
            catch (ApiException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation("User not found...");
                }
                else
                {
                    throw;
                }
            }

            return null;
        }
    }
}
