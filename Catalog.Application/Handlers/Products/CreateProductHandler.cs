using Catalog.Application.Commands.Products;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CreateProductHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = CatalogMapper.Mapper.Map<Product>(request.Payload);

            if (newProduct == null) 
            {
                throw new ArgumentException("Product details not provided.");      
            }

            //Create new Product
            await  _unitOfWork.Repository<Product>().AddAsync(newProduct);

            var productResponse = CatalogMapper.Mapper.Map<ProductResponse>(newProduct);

            return productResponse;

        }
    }
}
