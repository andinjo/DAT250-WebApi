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
    [Route("api/post/{postId}/reply")]
    public class ReplyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReplyService _replyService;

        public ReplyController(IMapper mapper, IReplyService replyService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _replyService = replyService ?? throw new ArgumentNullException(nameof(replyService));
        }

        [HttpGet]
        public async Task<ActionResult<List<ReplyResponse>>> List(int postId)
        {
            var replies = await _replyService.List(postId);
            var response = _mapper.Map<List<ReplyResponse>>(replies);

            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ReplyResponse>> Create(int postId, CreateReply create)
        {
            var reply = await _replyService.Create(postId, create);
            var response = _mapper.Map<ReplyResponse>(reply);

            return Created($"api/post/{postId}/reply/{response.Id}", response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReplyResponse>> Create(int postId, int id)
        {
            var reply = await _replyService.Read(id);
            var response = _mapper.Map<ReplyResponse>(reply);

            return Ok(response);
        }

        [HttpPost("{replyId}"), Authorize]
        public async Task<ActionResult<ReplyResponse>> Create(int postId, int replyId, UpdateReply update)
        {
            var reply = await _replyService.Update(replyId, update);
            var response = _mapper.Map<ReplyResponse>(reply);

            return Ok(response);
        }

        [HttpPost("{replyId}"), Authorize]
        public async Task<ActionResult> Delete(int postId, int replyId)
        {
            await _replyService.Delete(replyId);

            return NoContent();
        }
    }
}
