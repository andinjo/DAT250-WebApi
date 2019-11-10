using AutoMapper;
using Models;

namespace Web.Test
{
    public abstract class ModelBaseTest
    {
        protected readonly IMapper Mapper;

        protected ModelBaseTest()
        {
            var mapperConfig = new MapperConfiguration(config =>
                config.AddProfile(typeof(MapperProfile)));
            mapperConfig.AssertConfigurationIsValid();

            Mapper = new Mapper(mapperConfig);
        }
    }
}
