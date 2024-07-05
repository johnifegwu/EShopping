
using Catalog.Application.Commands.Types;
using Catalog.Core.Entities;
using Catalog.Core.Exceptions;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Types
{
    public class DeleteTypeHandler : IRequestHandler<DeleteTypeCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteTypeHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
        {
            var type = await Task.FromResult(_unitOfWork.Repository<ProductType>().Get().Where(x => x.Id == new ObjectId(request.TypeId)).FirstOrDefault());

            if(type == null)
            {
                throw new RecordNotFoundException("Type not found.");
            }

            var rowsAffected = await _unitOfWork.Repository<ProductType>().DeleteAsync(type, cancellationToken);

            return rowsAffected > 0;
        }
    }
}
