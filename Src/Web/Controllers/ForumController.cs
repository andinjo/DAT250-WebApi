using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Core;
using Models.Requests;
using Models.Responses;
using Services;
using Services.ClientWrappers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IForumService _forumService;
        private readonly IUserClientWrapper _userClient;
        private readonly IUserService _userService;

        public ForumController(
            IMapper mapper,
            IForumService forumService,
            IUserClientWrapper userClient,
            IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
            _userClient = userClient ?? throw new ArgumentNullException(nameof(userClient));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ForumResponse>>> List()
        {
            var forumList = await _forumService.List();
            var response = _mapper.Map<List<ForumResponse>>(forumList);

            var users = (await Task.WhenAll(GetUsers(forumList))).ToList();

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

            var user = await ReadUser(forum.UserId);
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

        private IEnumerable<Task<User>> GetUsers(IEnumerable<Forum> forums)
        {
            var userIds = forums.Select(f => f.UserId).Distinct();

            foreach (var userId in userIds)
            {
                yield return ReadUser(userId);
            }
        }

        private Task<User> ReadUser(string userId)
        {
            return _userClient.Get(userId);
        }
    }
}
