using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace Repositories.Forum
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions options) : base(options) { }

        public DbSet<Models.Core.Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }
}
