using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Repositories;

namespace Services.Test.ReplyServiceTest
{
    public abstract class BaseReplyTest
    {
        protected readonly IMapper Mapper;
        protected readonly IPostService PostService;
        protected readonly IReplyRepository ReplyRepository;
        protected readonly IUserService User;

        protected readonly IReplyService ReplyService;

        protected BaseReplyTest()
        {
            var logger =  A.Fake<ILogger<ReplyService>>();
            Mapper = A.Fake<IMapper>();
            PostService = A.Fake<IPostService>();
            ReplyRepository = A.Fake<IReplyRepository>();
            User = A.Fake<IUserService>();

            ReplyService = new ReplyService(
                logger,
                Mapper,
                PostService,
                ReplyRepository,
                User
            );
        }
    }
}
