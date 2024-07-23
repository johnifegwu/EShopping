
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
            CreateMap<UserAddress, AddressRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
        }
    }
}
