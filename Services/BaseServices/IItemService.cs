using System.Threading.Tasks;
using Models.Requests;
using Models.Responses;

namespace Services.BaseServices
{
    public interface IItemService
    {
        Task<ItemResponse> Create(CreateItemRequest request);
        Task<ItemResponse> Update(UpdateItemRequest request);
        Task<ItemResponse> Read(int id);
        Task<bool> Delete(int id);
    }
}
