
using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            //setup mapping for responses
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        }
    }
}
