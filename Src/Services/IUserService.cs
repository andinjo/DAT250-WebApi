using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Core;

namespace Services
{
    public interface IUserService
    {
        User Auth();

        string Id();

        bool Is(string userId);

        bool Exists();

        Task<User> Read(string userId);

        IEnumerable<Task<User>> List(IEnumerable<string> userIds);
    }
}
