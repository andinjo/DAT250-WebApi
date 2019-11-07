using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Web.Requests;
using Web.Responses;

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

        [HttpPost]
        public async Task<ActionResult<ForumResponse>> Create([FromBody] CreateForum request)
        {
            var forum = _mapper.Map<Forum>(request);
            var result = await _forumService.Create(forum);
            var response = _mapper.Map<ForumResponse>(result);

            return new OkObjectResult(response);
        }
    }
}
