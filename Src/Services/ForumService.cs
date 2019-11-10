using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.Business;
using Models.Requests;
using Newtonsoft.Json;
using Repositories;

namespace Services
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly ILogger<ForumService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _user;

        public ForumService(
            IForumRepository forumRepository,
            ILogger<ForumService> logger,
            IMapper mapper,
            IUserService userService)
        {
            _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _user = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public Task<List<Forum>> List()
        {
            _logger.LogInformation("Listing forums");
            return _forumRepository.List();
        }

        public async Task<Forum> Create(CreateForum create)
        {
            _logger.LogInformation($"Creating forum {JsonConvert.SerializeObject(create)}");

            if (!_user.Exists())
            {
                _logger.LogInformation("User is not authenticated");
                return null;
            }

            var forum = _mapper.Map<Forum>(create);
            forum.UserId = _user.Id();
            await _forumRepository.Create(forum);

            _logger.LogInformation($"Created forum with ID {forum.Id}");
            return forum;
        }

        public Task<Forum> Read(int id)
        {
            _logger.LogInformation($"Reading forum {id}");
            return _forumRepository.Read(id);
        }

        public async Task<Forum> Update(int id, UpdateForum update)
        {
            var forum = await _forumRepository.Read(id);
            _mapper.Map(update, forum);

            if (!CanEdit(forum))
            {
                _logger.LogInformation($"User cannot update forum {id}");
                return null;
            }

            _logger.LogInformation($"Updating forum {id} {JsonConvert.SerializeObject(update)}");
            await _forumRepository.Update(forum);
            return forum;
        }

        public async Task Delete(int id)
        {
            var forum = await _forumRepository.Read(id);

            if (!CanEdit(forum))
            {
                _logger.LogInformation($"User cannot delete forum {id}");
                return;
            }

            _logger.LogInformation($"Deleting forum {forum.Id}");
            await _forumRepository.Delete(forum);
        }

        private bool CanEdit(Forum forum)
        {
            return forum != null && _user.Is(forum.UserId);
        }
    }
}
