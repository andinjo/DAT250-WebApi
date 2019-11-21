using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace Repositories.Forum
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
            return await _context
                .Posts
                .Where(p => p.Forum.Id == forumId)
                .ToListAsync();
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
