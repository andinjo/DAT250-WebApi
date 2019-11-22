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
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IForumService _forumService;
        private readonly IUserService _userService;

        public ForumController(
            IMapper mapper,
            IForumService forumService,
            IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ForumResponse>>> List()
        {
            var forumList = await _forumService.List();
            var response = _mapper.Map<List<ForumResponse>>(forumList);

            var userIds = forumList.Select(f => f.UserId).Distinct();
            var users = await Task.WhenAll(_userService.List(userIds));

            response = response.Select(forum =>
            {
                var userId = forumList.First(f => f.Id == forum.Id).UserId;
                var username = users.First(u => u.Id == userId).Username;

                forum.Owner = username;

                return forum;
            }).ToList();

            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ForumResponse>> Create([FromBody] CreateForum create)
        {
            var forum = await _forumService.Create(create);
            if (forum == null)
            {
                return Unauthorized();
            }

            var response = _mapper.Map<ForumResponse>(forum);
            response.Owner = _userService.Auth().Username;

            return Created($"/api/forum/{response.Id}", response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ForumResponse>> Read(int id)
        {
            var forum = await _forumService.Read(id);
            if (forum == null)
            {
                return NotFound();
            }

            var user = await _userService.Read(forum.UserId);
            var response = _mapper.Map<ForumResponse>(forum);
            response.Owner = user?.Username ?? "[User deleted]";

            return Ok(response);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<ForumResponse>> Update(int id, [FromBody] UpdateForum update)
        {
            var forum = await _forumService.Update(id, update);
            if (forum == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ForumResponse>(forum);
            response.Owner = _userService.Auth().Username;
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            await _forumService.Delete(id);

            return NoContent();
        }
    }
}
