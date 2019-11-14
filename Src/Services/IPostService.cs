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

        Task<Post> Read(int postId);

        Task<Post> Update(int postId, UpdatePost update);

        Task Delete(int postId);
    }
}
