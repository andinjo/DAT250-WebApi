using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Core;
using Models.Requests;

namespace Services
{
    public interface IReplyService
    {
        Task<List<Reply>> List(int postId);

        Task<Reply> Create(int postId, CreateReply create);

        Task<Reply> Read(int id);

        Task<Reply> Update(int id, UpdateReply update);

        Task Delete(int id);
    }
}
