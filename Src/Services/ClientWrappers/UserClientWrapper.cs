using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Clients;
using Microsoft.Extensions.Logging;
using Models.Core;
using Refit;

namespace Services.ClientWrappers
{
    public class UserClientWrapper : IUserClientWrapper
    {
        private readonly IUserClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<UserClientWrapper> _logger;

        public UserClientWrapper(
            IUserClient client,
            IMapper mapper,
            ILogger<UserClientWrapper> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Get(string userId)
        {
            _logger.LogInformation($"Fetching request for user {userId}");
            try
            {
                var response = await _client.Get(userId);
                var user = _mapper.Map<User>(response);

                _logger.LogInformation($"Found user {user.Username}");

                return user;
            }
            catch (ApiException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogInformation($"Could not find user {userId}");
                }
            }

            return null;
        }
    }
}
