using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IForumService _forumService;

        public ForumController(IMapper mapper, IForumService forumService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ForumResponse>>> List()
        {
            var forums = await _forumService.List();
            var response = _mapper.Map<List<ForumResponse>>(forums);

            return new OkObjectResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<ForumResponse>> Create([FromBody] CreateForum create)
        {
            var forum = await _forumService.Create(create);
            var response = _mapper.Map<ForumResponse>(forum);

            return new CreatedResult($"/api/forum/{response.Id}", response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ForumResponse>> Read(int id)
        {
            var forum = await _forumService.Read(id);
            if (forum == null)
            {
                return new NotFoundResult();
            }

            var response = _mapper.Map<ForumResponse>(forum);
            return new OkObjectResult(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ForumResponse>> Update(int id, UpdateForum update)
        {
            var forum = await _forumService.Update(id, update);
            if (forum == null)
            {
                return new NotFoundResult();
            }

            var response = _mapper.Map<ForumResponse>(forum);
            return new OkObjectResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _forumService.Delete(id);

            return new NoContentResult();
        }
    }
}
