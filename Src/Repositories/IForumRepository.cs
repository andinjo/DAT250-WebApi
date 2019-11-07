using System.Threading.Tasks;
using Models;

namespace Repositories
{
    public interface IForumRepository
    {
        Task List(string userId);

        Task Create(Forum forum);

        Task<Forum> Read(int id);

        Task Update(Forum forum);

        Task Delete(Forum forum);
    }
}
