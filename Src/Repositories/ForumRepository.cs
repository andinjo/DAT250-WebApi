using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace Repositories
{
    public class ForumRepository : IForumRepository
    {
        private readonly ForumContext _context;

        public ForumRepository(ForumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Forum>> List()
        {
            return _context.Forums.ToListAsync();
        }

        public Task Create(Forum forum)
        {
            _context.Forums.Add(forum);
            return _context.SaveChangesAsync();
        }

        public async Task<Forum> Read(int id)
        {
            return await _context.Forums.FindAsync(id);
        }

        public Task Update(Forum forum)
        {
            _context.Forums.Update(forum);
            return _context.SaveChangesAsync();
        }

        public Task Delete(Forum forum)
        {
            _context.Forums.Remove(forum);
            return _context.SaveChangesAsync();
        }
    }
}
