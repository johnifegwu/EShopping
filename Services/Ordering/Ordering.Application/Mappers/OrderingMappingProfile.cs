
using AutoMapper;
using Ordering.Application.Requests;
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

            //Setup mapping for requests
            CreateMap<Order, CreateOrderRequest>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetail>().ReverseMap();
            CreateMap<Order, UpdateOrderRequest>().ReverseMap();
        }
    }
}
