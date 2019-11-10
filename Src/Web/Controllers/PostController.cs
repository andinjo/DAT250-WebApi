using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Business;
using Models.Requests;
using Services;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/forum/{forumId}/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        [HttpGet]
        public async Task<ActionResult<List<Post>>> List(int forumId)
        {
            return Ok(await _postService.List(forumId));
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Post>> Create(int forumId, CreatePost create)
        {
            var post = await _postService.Create(forumId, create);

            return Created($"api/forum/{forumId}/post/{post.Id}", post);
        }
    }
}
