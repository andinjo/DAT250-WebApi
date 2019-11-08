using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Business;

namespace Repositories
{
    public interface IForumRepository
    {
        Task<List<Forum>> List();

        Task Create(Forum forum);

        Task<Forum> Read(int id);

        Task Update(Forum forum);

        Task Delete(Forum forum);
    }
}
