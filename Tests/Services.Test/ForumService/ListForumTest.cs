﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FakeItEasy;
using Models.Business;
using Xunit;

namespace Services.Test.ForumService
{
    public class ListForumTest : ForumServiceTest
    {
        [Fact]
        public async Task List_FetchesAllForums()
        {
            A.CallTo(() => ForumRepository.List())
                .Returns(new List<Forum> {new Forum()});

            var list = await ForumService.List();

            Assert.NotEmpty(list);
            A.CallTo(() => ForumRepository.List())
                .MustHaveHappenedOnceExactly();
        }
    }
}
