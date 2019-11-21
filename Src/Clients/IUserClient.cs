using System.Threading.Tasks;
using Models.Responses;
using Refit;

namespace Clients
{
    public interface IUserClient
    {
        [Get("/api/v2/users/{userId}")]
        Task<UserResponse> Get(string userId);
    }
}
