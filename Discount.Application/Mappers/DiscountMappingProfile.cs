
using AutoMapper;
using Discount.Application.Requests;
using Discount.Application.Responses;
using Discount.Core.Entities;

namespace Discount.Application.Mappers
{
    public class DiscountMappingProfile : Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<Coupon, CouponResponse>().ReverseMap();
            CreateMap<Coupon, CreateCouponRequest>().ReverseMap();
            CreateMap<Coupon, UpdateCouponRequest>().ReverseMap();
        }
    }
}
