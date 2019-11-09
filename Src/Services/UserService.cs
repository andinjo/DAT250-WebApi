using System;
using System.Linq;
using System.Security.Claims;
using Models.Business;

namespace Services
{
    public class UserService : IUserService
    {
        private const string NameKey = "nickname";
        private const string EmailKey = "name";

        private readonly ClaimsPrincipal _claims;

        public UserService(ClaimsPrincipal claims)
        {
            _claims = claims ?? throw new ArgumentNullException(nameof(claims));
        }

        public User Auth()
        {
            if (Exists()) return null;

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

        private string GetClaim(string identifier)
        {
            return _claims
                .Claims
                .FirstOrDefault(claim => claim.Type == identifier)
                ?.Value;
        }
    }
}
