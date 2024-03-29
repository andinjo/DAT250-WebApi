﻿using AutoMapper;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Repositories.Forum;

namespace Services.Test.ForumServiceTest
{
    public abstract class BaseForumTest
    {
        protected readonly IForumRepository ForumRepository;
        protected readonly IMapper Mapper;
        protected readonly IUserService UserService;

        protected readonly IForumService ForumService;

        protected BaseForumTest()
        {
            ForumRepository = A.Fake<IForumRepository>();
            var logger = A.Fake<ILogger<ForumService>>();
            Mapper = A.Fake<IMapper>();
            UserService = A.Fake<IUserService>();

            ForumService = new ForumService(
                ForumRepository,
                logger,
                Mapper,
                UserService);
        }
    }
}
