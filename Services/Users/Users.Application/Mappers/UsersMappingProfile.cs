
using AutoMapper;
using Users.Application.Requests;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Ordering.Application.Mappers
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            //Setup mapping for responses
            CreateMap<UserAddress, CreateUserAddressRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<UserAddress, UserAddressResponse>().ReverseMap();
            CreateMap<AddressType, AddressTypeResponse>().ReverseMap();
        }
    }
}
