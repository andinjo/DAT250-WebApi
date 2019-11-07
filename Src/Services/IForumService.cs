using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IForumService
    {
        Task<Forum> Create(Forum forum);
        Task<Forum> Read(int id);
        Task<Forum> Update(Forum forum);
        Task Delete(Forum forum);
    }
}
