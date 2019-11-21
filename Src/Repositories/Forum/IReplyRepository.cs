using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Core;

namespace Repositories.Forum
{
    public interface IReplyRepository
    {
        Task<List<Reply>> List(int postId);

        Task<Reply> Create(Reply reply);

        Task<Reply> Read(int id);

        Task Update(Reply reply);

        Task Delete(Reply reply);
    }
}
