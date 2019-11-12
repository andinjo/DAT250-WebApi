using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Core;
using Models.Requests;

namespace Services
{
    public interface IForumService
    {
        Task<List<Forum>> List();
        Task<Forum> Create(CreateForum create);
        Task<Forum> Read(int id);
        Task<Forum> Update(int id, UpdateForum update);
        Task Delete(int id);
    }
}
