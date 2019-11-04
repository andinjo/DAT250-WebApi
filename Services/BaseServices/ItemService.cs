using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.Dto;
using Models.Requests;
using Models.Responses;
using Repositories.Repositories;

namespace Services.BaseServices
{
    public class ItemService : IItemService
    {

        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;
        private readonly IItemRepository _itemRepo;

        public ItemService(IMapper mapper, ILogger<ItemService> logger, IItemRepository itemRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemRepo = itemRepo ?? throw new ArgumentNullException(nameof(itemRepo));
        }


        public async Task<ItemResponse> Create(CreateItemRequest request)
        {
            _logger.LogInformation("Executed query for create new item.");
            var item = await _itemRepo.Create(_mapper.Map<Item>(request));

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<ItemResponse> Update(UpdateItemRequest request)
        {
            _logger.LogInformation("Executed query for update an item.");
            var item = await _itemRepo.Update(_mapper.Map<Item>(request));

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<ItemResponse> Read(int id)
        {
            _logger.LogInformation($"Executed query for reading item with id = {id}.");
            var item = await _itemRepo.Read(id);
            if(item == null)
            {
                _logger.LogInformation("Could not find item");
                return null;
            }

            _logger.LogInformation("Item found successfully");
            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<bool> Delete(int id)
        {
            _logger.LogInformation($"Executed query for deleting item with id = {id}.");
            var isDeleted = await _itemRepo.Delete(id);

            var message = isDeleted ? $"Item successfully removed" : "Could not find item";
            _logger.LogInformation(message);

            return isDeleted;
        }
    }
}
