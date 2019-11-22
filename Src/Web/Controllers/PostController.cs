using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserService _userService;

        public PostController(
            IMapper mapper,
            IPostService postService,
            IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<List<PostResponse>>> List(int forumId)
        {
            var posts = await _postService.List(forumId);
            var response = _mapper.Map<List<PostResponse>>(posts);

            var userIds = posts.Select(p => p.UserId).Distinct();
            var users = await Task.WhenAll(_userService.List(userIds));

            response = response.Select(post =>
            {
                var userId = posts.First(p => p.Id == post.Id).UserId;
                var username = users.First(u => u.Id == userId).Username;
                post.Author = username;

                return post;
            }).ToList();

            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<PostResponse>> Create(int forumId, CreatePost create)
        {
            var post = await _postService.Create(forumId, create);
            var response = _mapper.Map<PostResponse>(post);
            response.Author = _userService.Auth().Username;

            return Created($"api/forum/{forumId}/post/{response.Id}", response);
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostResponse>> Read(int forumId, int postId)
        {
            var post = await _postService.Read(postId);
            if (post == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PostResponse>(post);
            var user = await _userService.Read(post.UserId);
            response.Author = user.Username;

            return Ok(response);
        }

        [HttpPut("{postId}"), Authorize]
        public async Task<ActionResult<PostResponse>> Update(int forumId, int postId, UpdatePost update)
        {
            var post = await _postService.Update(postId, update);
            if (post == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<PostResponse>(post);
            response.Author = _userService.Auth().Id;

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
