using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.Core;
using Models.Requests;
using Newtonsoft.Json;
using Repositories;
using Repositories.Forum;

namespace Services
{
    public class PostService : IPostService
    {
        private readonly IForumService _forumService;
        private readonly ILogger<PostService> _logger;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;

        public PostService(
            IForumService forumService,
            ILogger<PostService> logger,
            IMapper mapper,
            IPostRepository postRepository,
            IUserService userService)
        {
            _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public Task<List<Post>> List(int forumId)
        {
            _logger.LogInformation($"Listing replies for forum {forumId}");
            return _postRepository.List(forumId);
        }

        public async Task<Post> Create(int forumId, CreatePost create)
        {
            _logger.LogInformation($"Creating post {JsonConvert.SerializeObject(create)}");
            if (!_userService.Exists())
            {
                _logger.LogWarning("User not authenticated");
                return null;
            }

            var forum = await _forumService.Read(forumId);
            if (forum == null)
            {
                _logger.LogWarning("Forum not found");
                return null;
            }

            var post = _mapper.Map<Post>(create);
            post.UserId = _userService.Id();
            post.Forum = forum;

            _logger.LogInformation($"Saving post {JsonConvert.SerializeObject(post)}");
            await _postRepository.Create(post);
            _logger.LogInformation($"Post was assigned ID {post.Id}");

            return post;
        }

        public async Task<Post> Read(int postId)
        {
            _logger.LogInformation($"Reading post {postId}");
            var post = await _postRepository.Read(postId);

            return post;
        }

        public async Task<Post> Update(int postId, UpdatePost update)
        {
            var oldPost = await _postRepository.Read(postId);
            if (oldPost == null)
            {
                _logger.LogWarning($"Post {postId} does not exist");
                return null;
            }

            _logger.LogInformation($"Updating post {JsonConvert.SerializeObject(update)}");
            if (!_userService.Is(oldPost.UserId))
            {
                _logger.LogWarning("User not authorized");
                return null;
            }

            var post = _mapper.Map(update, oldPost);
            post.UserId =_userService.Id();
            await _postRepository.Update(post);

            return post;
        }

        public async Task Delete(int postId)
        {
            var post = await _postRepository.Read(postId);
            if (post == null)
            {
                _logger.LogWarning($"Post with id {postId} not found");
                return;
            }
            if (!_userService.Is(post.UserId))
            {
                _logger.LogWarning("User not authorized");
                return;
            }

            await _postRepository.Delete(post);
        }
    }
}
