using AutoMapper;
using Models.Dto;
using Models.Requests;
using Models.Responses;

namespace Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            // Requests
            CreateMap<CreateItemRequest, Item>();
            CreateMap<UpdateItemRequest, Item>();

            // Responses
            CreateMap<Item, ItemResponse>();
        }
    }
}
