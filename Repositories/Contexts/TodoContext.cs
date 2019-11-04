using Microsoft.EntityFrameworkCore;
using Models.Dto;

namespace Repositories.Contexts
{
    public class TodoContext : DbContext 
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { 
        }

        // might be overkill
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoList>().HasMany<Item>(todo => todo.Items);
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }
    }
}
