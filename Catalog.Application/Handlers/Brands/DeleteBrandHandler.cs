
using Catalog.Application.Commands.Brands;
using Catalog.Core.Entities;
using Catalog.Core.Exceptions;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Brands
{
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteBrandHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await Task.FromResult(_unitOfWork.Repository<ProductBrand>().Get().Where(x => x.Id == new ObjectId(request.BrandId)).FirstOrDefault());

            if(brand == null)
            {
                throw new RecordNotFoundException("Brand not found.");
            }

            var rowsAffected = await _unitOfWork.Repository<ProductBrand>().DeleteAsync(brand, cancellationToken);

            return rowsAffected > 0;
        }
    }
}
