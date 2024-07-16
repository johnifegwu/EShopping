
using AutoMapper;
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile() 
        { 
            //Setup mapping for responses
            CreateMap<ProductBrand, BrandResponse>().ReverseMap();
            CreateMap<ProductType, TypeResponse>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();

            //Setup mapping for create requests
            CreateMap<ProductBrand, CreateBrandRequest>().ReverseMap();
            CreateMap<ProductType, CreateTypeRequest>().ReverseMap();
            CreateMap<Product, CreateProductRequest>().ReverseMap();

            //Setup mapping for update requests
            CreateMap<ProductBrand, UpdateBrandRequest>().ReverseMap();
            CreateMap<ProductType, UpdateTypeRequest>().ReverseMap();
            CreateMap<Product, UpdateProductRequest>().ReverseMap();
        }
    }
}
