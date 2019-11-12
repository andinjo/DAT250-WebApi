using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Core;
using Models.Requests;

namespace Services
{
    public interface IPostService
    {
        Task<List<Post>> List(int forumId);

        Task<Post> Create(int forumId, CreatePost create);
    }
}
