
using Catalog.Application.Commands.Products;
using Catalog.Core.Entities;
using eShopping.Exceptions;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Products
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteProductHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Task.FromResult(_unitOfWork.Repository<Product>().Get().Where(x => x.Id == new ObjectId(request.ProductId)).FirstOrDefault());

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            var rowsAffected = await _unitOfWork.Repository<Product>().DeleteAsync(product, cancellationToken);

            return rowsAffected > 0;
        }
    }
}
