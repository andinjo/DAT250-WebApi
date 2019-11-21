using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Forum
{
    public class ForumRepository : IForumRepository
    {
        private readonly ForumContext _context;

        public ForumRepository(ForumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Models.Core.Forum>> List()
        {
            return _context.Forums.ToListAsync();
        }

        public Task Create(Models.Core.Forum forum)
        {
            _context.Forums.Add(forum);
            return _context.SaveChangesAsync();
        }

        public async Task<Models.Core.Forum> Read(int id)
        {
            return await _context.Forums.FindAsync(id);
        }

        public Task Update(Models.Core.Forum forum)
        {
            _context.Forums.Update(forum);
            return _context.SaveChangesAsync();
        }

        public Task Delete(Models.Core.Forum forum)
        {
            _context.Forums.Remove(forum);
            return _context.SaveChangesAsync();
        }
    }
}
