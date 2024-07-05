
using Catalog.Application.Commands.Brands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Exceptions;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Brands
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, BrandResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateBrandHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Payload.Name))
            {
                throw new ArgumentException("Name not provided.");
            }

            if (string.IsNullOrWhiteSpace(request.Payload.Id))
            {
                throw new ArgumentException("Id not provided.");
            }

            var brand = await Task.FromResult(_unitOfWork.Repository<ProductBrand>().Get().Where(x => x.Id == new ObjectId(request.Payload.Id)).FirstOrDefault());  

            if (brand == null)
            {
                throw new RecordNotFoundException("Brand not found.");
            }

            //Update Brand
            brand.Name = request.Payload.Name;
            await _unitOfWork.Repository<ProductBrand>().UpdateAsync(brand, cancellationToken);

            var response = CatalogMapper.Mapper.Map<BrandResponse>(brand);

            return response;
        }
    }
}
