
using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Core.Entities;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, DiscountModel>().ReverseMap();
            CreateMap<Coupon, CreateDiscountModel>().ReverseMap();
        }
    }
}
