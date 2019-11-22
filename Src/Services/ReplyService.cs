using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.Core;
using Models.Requests;
using Models.Responses;
using Repositories.Forum;

namespace Services
{
    public class ReplyService : IReplyService
    {
        private readonly ILogger<ReplyService> _logger;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IReplyRepository _replyRepository;
        private readonly IUserService _userService;

        public ReplyService(
            ILogger<ReplyService> logger,
            IMapper mapper,
            IPostService postService,
            IReplyRepository replyRepository,
            IUserService user)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _replyRepository = replyRepository ?? throw new ArgumentNullException(nameof(replyRepository));
            _userService = user ?? throw new ArgumentNullException(nameof(user));
        }

        public Task<List<Reply>> List(int postId)
        {
            return _replyRepository.List(postId);
        }

        public async Task<Reply> Create(int postId, CreateReply create)
        {
            if (!_userService.Exists())
            {
                _logger.LogWarning("User not authenticated");
                return null;
            }
            var post = await _postService.Read(postId);
            if (post == null)
            {
                _logger.LogWarning($"Post {postId} not found");
                return null;
            }

            var reply = _mapper.Map<Reply>(create);
            reply.UserId = _userService.Id();
            reply.Post = post;

            await _replyRepository.Create(reply);

            return reply;
        }

        public Task<Reply> Read(int id)
        {
            return _replyRepository.Read(id);
        }

        public async Task<Reply> Update(int id, UpdateReply update)
        {
            var oldReply = await _replyRepository.Read(id);
            if (oldReply == null)
            {
                _logger.LogWarning($"Reply {id} not found");
                return null;
            }
            if (!_userService.Is(oldReply.UserId))
            {
                _logger.LogWarning("User is authorized");
                return null;
            }
            var reply = _mapper.Map(update, oldReply);

            await _replyRepository.Update(reply);

            return reply;
        }

        public async Task Delete(int id)
        {
            var reply = await _replyRepository.Read(id);
            if (reply == null)
            {
                _logger.LogWarning($"Reply {id} not found");
                return;
            }

            if (!_userService.Is(reply.UserId))
            {
                _logger.LogWarning("User not authorized");
                return;
            }

            await _replyRepository.Delete(reply);
        }
    }
}
