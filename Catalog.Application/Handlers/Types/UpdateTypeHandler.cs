
using Catalog.Application.Commands.Types;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Exceptions;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Types
{
    public class UpdateTypeHandler : IRequestHandler<UpdateTypeCommand, TypeResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateTypeHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TypeResponse> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Payload.Name))
            {
                throw new ArgumentException("Name not provided.");
            }

            if (string.IsNullOrWhiteSpace(request.Payload.Id))
            {
                throw new ArgumentException("Id not provided.");
            }

            var type = await Task.FromResult(_unitOfWork.Repository<ProductType>().Get().Where(x => x.Id == new ObjectId(request.Payload.Id)).FirstOrDefault());

            if(type == null)
            {
                throw new RecordNotFoundException("Type not found.");
            }

            //Update type
            type.Name = request.Payload.Name;
            await _unitOfWork.Repository<ProductType>().UpdateAsync(type, cancellationToken);

            var response = CatalogMapper.Mapper.Map<TypeResponse>(type);

            return response;
        }
    }
}
