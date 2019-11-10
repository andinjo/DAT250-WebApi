using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Business;

namespace Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumContext _context;

        public PostRepository(ForumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Post>> List(int forumId)
        {
            var forum = await _context.Forums.FindAsync(forumId);
            return forum.Posts.ToList();
        }

        public async Task<Post> Read(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> Create(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public Task Update(Post post)
        {
            _context.Posts.Update(post);
            return _context.SaveChangesAsync();
        }

        public Task Delete(Post post)
        {
            _context.Remove(post);
            return _context.SaveChangesAsync();
        }
    }
}
