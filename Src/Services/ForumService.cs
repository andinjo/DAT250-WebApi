using System;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository ?? throw new ArgumentNullException(nameof(forumRepository));
        }

        public async Task<Forum> Create(Forum forum)
        {
            await _forumRepository.Create(forum);
            return forum;
        }

        public Task<Forum> Read(int id)
        {
            return _forumRepository.Read(id);
        }

        public async Task<Forum> Update(Forum forum)
        {
            await _forumRepository.Update(forum);
            return forum;
        }

        public Task Delete(Forum forum)
        {
            return _forumRepository.Delete(forum);
        }
    }
}
