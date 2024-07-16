using Catalog.Application.Commands.Brands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Brands
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, BrandResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CreateBrandHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = CatalogMapper.Mapper.Map<ProductBrand>(request.Payload);

            if (brand == null)
            {
                throw new ArgumentException("Brand details not provided.");
            }

            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentException("Name not provided.");
            }

            //Create brand
            await _unitOfWork.Repository<ProductBrand>().UpdateAsync(brand, cancellationToken);

            var response = CatalogMapper.Mapper.Map<BrandResponse>(brand);

            return response;
        }
    }
}
