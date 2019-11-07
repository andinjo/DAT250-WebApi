using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ForumContext : DbContext
    {
        public ForumContext(DbContextOptions options) : base(options) { }

        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }
}
