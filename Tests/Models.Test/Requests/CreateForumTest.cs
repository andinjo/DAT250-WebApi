using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Models;
using Models.Business;
using Models.Request;
using Xunit;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Web.Test.Requests
{
    public class CreateForumTest
    {
        private readonly IMapper _mapper;

        public CreateForumTest()
        {
            var mapperConfig = new MapperConfiguration(config =>
                config.AddProfile(typeof(MapperProfile)));
            mapperConfig.AssertConfigurationIsValid();

            _mapper = new Mapper(mapperConfig);
        }

        [Fact]
        public void MapToForum_FromCreateForum_FillsContentAndTitle()
        {
            var request = new CreateForum
            {
                Description = "A test forum",
                Title = "Test"
            };

            var forum = _mapper.Map<Forum>(request);

            Assert.Equal(request.Description, forum.Description);
            Assert.Equal(request.Title, forum.Title);
            Assert.True(forum.CreatedAt > DateTime.Now.AddMilliseconds(-10));
            Assert.True(forum.UpdatedAt > DateTime.Now.AddMilliseconds(-10));
        }

        [Fact]
        public void ValidateCreateForum_DescriptionTooLong_ValidationReturnsFalse()
        {
            var request = new CreateForum
            {
                Description = new string('a', 1024),
                Title = "Test"
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Description)
            };
            var result = Validator.TryValidateProperty(request.Description, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateCreateForum_NoTitle_ValidationReturnsFalse()
        {
            var request = new CreateForum
            {
                Description = "Description"
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Title)
            };

            var result = Validator.TryValidateProperty(request.Title, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateCreateForum_TitleTooShort_ValidationReturnsFalse()
        {
            var request = new CreateForum
            {
                Description = "Description",
                Title = "A"
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Title)
            };

            var result = Validator.TryValidateProperty(request.Title, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateCreateForum_TitleTooLong_ValidationReturnsFalse()
        {
            var request = new CreateForum
            {
                Description = "Description",
                Title = new string('A', 64)
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Title)
            };

            var result = Validator.TryValidateProperty(request.Title, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateCreateForum_TitleContainsInvalidCharacters_ValidationReturnsFalse()
        {
            var request = new CreateForum
            {
                Description = "Description",
                Title = "illegal space"
            };

            var context = new ValidationContext(request)
            {
                MemberName = nameof(request.Title)
            };

            var result = Validator.TryValidateProperty(request.Title, context, null);

            Assert.False(result);
        }

        [Fact]
        public void ValidateCreateForum_FormIsValid_ValidationReturnsTrue()
        {
            var request = new CreateForum
            {
                Description = "Description",
                Title = "Test1"
            };

            var context = new ValidationContext(request);
            var result = Validator.TryValidateObject(request, context, null, true);

            Assert.True(result);
        }
    }
}
