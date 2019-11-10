using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Repositories;

namespace Services.Test.PostServiceTest
{
    public abstract class BasePostTest
    {
        protected readonly IForumService ForumService;
        protected readonly IMapper Mapper;
        protected readonly IPostRepository PostRepository;
        protected readonly IUserService User;

        protected readonly IPostService PostService;

        protected BasePostTest()
        {
            ForumService = A.Fake<IForumService>();
            Mapper = A.Fake<IMapper>();
            PostRepository = A.Fake<IPostRepository>();
            User = A.Fake<IUserService>();
            var logger = A.Fake<ILogger<PostService>>();

            PostService = new PostService(ForumService, logger, Mapper, PostRepository, User);
        }
    }
}
