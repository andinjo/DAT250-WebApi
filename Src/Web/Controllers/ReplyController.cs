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
    [Route("api/post/{postId}/reply")]
    public class ReplyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReplyService _replyService;
        private readonly IUserService _userService;

        public ReplyController(
            IMapper mapper,
            IReplyService replyService,
            IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _replyService = replyService ?? throw new ArgumentNullException(nameof(replyService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ReplyResponse>>> List(int postId)
        {
            var replies = await _replyService.List(postId);
            var response = _mapper.Map<List<ReplyResponse>>(replies);

            var userIds = replies.Select(f => f.UserId).Distinct();
            var users = await Task.WhenAll(_userService.List(userIds));

            response = response.Select(reply =>
            {
                var userId = replies.First(r => r.Id == reply.Id).UserId;
                var username = users.First(u => u.Id == userId).Username;

                reply.Author = username;

                return reply;
            }).ToList();

            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ReplyResponse>> Create(int postId, CreateReply create)
        {
            var reply = await _replyService.Create(postId, create);
            if (reply == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ReplyResponse>(reply);
            response.Author = _userService.Auth().Username;

            return Created($"api/post/{postId}/reply/{response.Id}", response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyResponse>> Read(int postId, int id)
        {
            var reply = await _replyService.Read(id);
            var response = _mapper.Map<ReplyResponse>(reply);
            var user = await _userService.Read(reply.UserId);
            response.Author = user.Username;

            return Ok(response);
        }

        [HttpPut("{replyId}"), Authorize]
        public async Task<ActionResult<ReplyResponse>> Update(int postId, int replyId, UpdateReply update)
        {
            var reply = await _replyService.Update(replyId, update);
            if (reply == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ReplyResponse>(reply);
            response.Author = _userService.Auth().Username;

            return Ok(response);
        }

        [HttpDelete("{replyId}"), Authorize]
        public async Task<ActionResult> Delete(int postId, int replyId)
        {
            await _replyService.Delete(replyId);

            return NoContent();
        }
    }
}
