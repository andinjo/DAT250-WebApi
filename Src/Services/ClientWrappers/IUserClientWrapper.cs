using System.Threading.Tasks;
using Models.Core;

namespace Services.ClientWrappers
{
    public interface IUserClientWrapper
    {
        Task<User> Get(string userId);
    }
}
