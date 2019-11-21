using System.Threading.Tasks;
using Models.Requests;
using Models.Responses;
using Refit;

namespace Clients
{
    public interface IAccessTokenClient
    {
        [Post("/oauth/token")]
        Task<TokenResponse> Token([Body] TokenRequest tokenRequest);
    }
}
