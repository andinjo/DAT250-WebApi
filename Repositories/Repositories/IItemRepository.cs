using System.Threading.Tasks;
using Models.Dto;

namespace Repositories.Repositories
{
    public interface IItemRepository
    {
        Task<Item> Create(Item item);
        Task<Item> Read(int id);
        Task<Item> Update(Item item);
        Task<bool> Delete(int id);
    }
}
