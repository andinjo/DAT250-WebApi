using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Models.Business;
using Models.Request;
using Repositories;

namespace Services
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly IMapper _mapper;

        public ForumService(IForumRepository forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<List<Forum>> List()
        {
            return _forumRepository.List();
        }

        public async Task<Forum> Create(CreateForum create)
        {
            var forum = _mapper.Map<Forum>(create);

            await _forumRepository.Create(forum);
            return forum;
        }

        public Task<Forum> Read(int id)
        {
            return _forumRepository.Read(id);
        }

        public async Task<Forum> Update(int id, UpdateForum update)
        {
            var forum = await _forumRepository.Read(id);
            _mapper.Map(update, forum);

            await _forumRepository.Update(forum);
            return forum;
        }

        public async Task Delete(int id)
        {
            var forum = await _forumRepository.Read(id);
            if (forum != null)
            {
                await _forumRepository.Delete(forum);
            }
        }
    }
}
