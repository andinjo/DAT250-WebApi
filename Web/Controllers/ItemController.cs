using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Models.Requests;
using Models.Responses;
using Services.BaseServices;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _baseService;

        public ItemController(IItemService baseService)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Create(CreateItemRequest request)
        {
            var item = await _baseService.Create(request);
            return new JsonResult(item);
        }

        [HttpPut]
        public async Task<ActionResult<Item>> Update(UpdateItemRequest request) {
            var item = await _baseService.Update(request);
            return new JsonResult(item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Read(int id)
        {
            var item = await _baseService.Read(id);
            return new JsonResult(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> Delete(int id)
        {
            await _baseService.Delete(id);
            return new NoContentResult();
        }
    }
}