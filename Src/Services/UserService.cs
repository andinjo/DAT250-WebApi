using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Models.Core;
using Services.ClientWrappers;

namespace Services
{
    public class UserService : IUserService
    {
        private const string NameKey = "nickname";
        private const string EmailKey = "name";

        private readonly ClaimsPrincipal _claims;
        private readonly IUserClientWrapper _userClient;

        public UserService(ClaimsPrincipal claims, IUserClientWrapper userClient)
        {
            _claims = claims ?? throw new ArgumentNullException(nameof(claims));
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
        }

        public User Auth()
        {
            if (!Exists()) return null;

            return new User
            {
                Id = Id(),
                Email = GetClaim(EmailKey),
                Username = GetClaim(NameKey)
            };
        }

        public string Id()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }

        public bool Is(string userId)
        {
            return Exists() && Id() == userId;
        }

        public bool Exists()
        {
            return !string.IsNullOrEmpty(Id());
        }

        public Task<User> Read(string userId)
        {
            return _userClient.Get(userId);
        }

        public IEnumerable<Task<User>> List(IEnumerable<string> userIds)
        {
            return userIds.Select(Read);
        }

        private string GetClaim(string identifier)
        {
            return _claims
                .Claims
                .FirstOrDefault(claim => claim.Type == identifier)
                ?.Value;
        }
    }
}
