using Catalog.Application.Commands.Products;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateProductHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Payload.Id))
            {
                throw new ArgumentException("Id not provided.");
            }

            if (string.IsNullOrWhiteSpace(request.Payload.Name))
            {
                throw new ArgumentException("Name not provided.");
            }

            var product = await Task.FromResult(_unitOfWork.Repository<Product>().Get().Where(x => x.Id == new ObjectId(request.Payload.Id)).FirstOrDefault());
            
            if (product == null)
            {
                throw new ArgumentException("Product not found.");
            }

            //Update Product
            product.Name = request.Payload.Name;
            product.Description = request.Payload.Description;
            product.ImageFile = request.Payload.ImageFile;
            product.Price = request.Payload.Price;
            product.ProductBrandId = (request.Payload.ProductBrandId != null) ? new ObjectId(request.Payload.ProductBrandId) : product.ProductBrandId;
            product.ProductTypeId = (request.Payload.ProductTypeId != null) ? new ObjectId(request.Payload.ProductTypeId) : product.ProductTypeId;
            product.Summary = request.Payload.Summary;
            
            await _unitOfWork.Repository<Product>().UpdateAsync(product);

            var response = CatalogMapper.Mapper.Map<ProductResponse>(product);

            return response;
        }
    }
}
