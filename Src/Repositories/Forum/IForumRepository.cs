using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Forum
{
    public interface IForumRepository
    {
        Task<List<Models.Core.Forum>> List();

        Task Create(Models.Core.Forum forum);

        Task<Models.Core.Forum> Read(int id);

        Task Update(Models.Core.Forum forum);

        Task Delete(Models.Core.Forum forum);
    }
}
