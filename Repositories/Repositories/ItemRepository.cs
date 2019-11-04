using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Dto;
using Repositories.Contexts;

namespace Repositories.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly TodoContext _context;

        public ItemRepository(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Item> Create(Item item)
        {
            await _context.Items.AddAsync(item);
            return item;
        }

        public async Task<Item> Read(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Item> Update(Item item)
        {
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
