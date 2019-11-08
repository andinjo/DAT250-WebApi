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
            return new User
            {
                Id = GetClaim(ClaimTypes.NameIdentifier),
                Email = GetClaim(EmailKey),
                Username = GetClaim(NameKey)
            };
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
