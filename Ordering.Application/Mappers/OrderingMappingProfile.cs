
using AutoMapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers
{
    public class OrderingMappingProfile : Profile
    {
        public OrderingMappingProfile()
        {
            //Setup mapping for responses
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}
