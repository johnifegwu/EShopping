
using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile() 
        { 
            CreateMap<ProductBrand, BrandResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
