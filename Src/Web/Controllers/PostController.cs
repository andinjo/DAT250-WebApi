using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;
using Services;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/forum/{forumId}/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        [HttpGet]
        public async Task<ActionResult<List<PostResponse>>> List(int forumId)
        {
            var posts = await _postService.List(forumId);
            var response = _mapper.Map<List<PostResponse>>(posts);
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<PostResponse>> Create(int forumId, CreatePost create)
        {
            var post = await _postService.Create(forumId, create);
            var response = _mapper.Map<PostResponse>(post);

            return Created($"api/forum/{forumId}/post/{response.Id}", response);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostResponse>> Read(int forumId, int postId)
        {
            var post = await _postService.Read(postId);
            var response = _mapper.Map<PostResponse>(post);

            return Ok(response);
        }

        [HttpPut("{postId}"), Authorize]
        public async Task<ActionResult<PostResponse>> Update(int forumId, int postId, UpdatePost update)
        {
            var post = await _postService.Update(postId, update);
            var response = _mapper.Map<PostResponse>(post);

            return Ok(response);
        }

        [HttpDelete("{postId}"), Authorize]
        public async Task<ActionResult<PostResponse>> Delete(int forumId, int postId)
        {
            await _postService.Delete(postId);

            return NoContent();
        }
    }
}
