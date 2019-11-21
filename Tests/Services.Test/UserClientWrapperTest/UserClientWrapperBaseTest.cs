﻿using AutoMapper;
using Clients;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Services.ClientWrappers;

namespace Services.Test.UserClientWrapperTest
{
    public abstract class UserClientWrapperBaseTest
    {
        protected readonly IUserClient Client;
        protected readonly IMapper Mapper;

        protected readonly IUserClientWrapper ClientWrapper;

        protected UserClientWrapperBaseTest()
        {
            Client = A.Fake<IUserClient>();
            Mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<UserClientWrapper>>();

            ClientWrapper = new UserClientWrapper(Client, Mapper, logger);
        }
    }
}
