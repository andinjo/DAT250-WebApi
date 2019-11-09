using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Repositories;

namespace Services.Test.ForumService
{
    public abstract class ForumServiceTest
    {
        protected readonly IForumRepository ForumRepository;
        protected readonly IMapper Mapper;
        protected readonly IUserService User;

        protected readonly IForumService ForumService;

        protected ForumServiceTest()
        {
            ForumRepository = A.Fake<IForumRepository>();
            var logger = A.Fake<ILogger<Services.ForumService>>();
            Mapper = A.Fake<IMapper>();
            User = A.Fake<IUserService>();

            ForumService = new Services.ForumService(ForumRepository, logger, Mapper, User);
        }
    }
}
