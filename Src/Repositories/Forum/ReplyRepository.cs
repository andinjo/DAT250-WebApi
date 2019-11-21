using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace Repositories.Forum
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly ForumContext _context;

        public ReplyRepository(ForumContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Reply>> List(int postId)
        {
            return await _context
                .Replies
                .Where(r => r.Post.Id == postId)
                .ToListAsync();
        }

        public async Task<Reply> Create(Reply reply)
        {
            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            return reply;
        }

        public async Task<Reply> Read(int id)
        {
            return await _context.Replies.FindAsync(id);
        }

        public async Task Update(Reply reply)
        {
            _context.Replies.Update(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Reply reply)
        {
            _context.Replies.Remove(reply);
            await _context.SaveChangesAsync();
        }
    }
}
