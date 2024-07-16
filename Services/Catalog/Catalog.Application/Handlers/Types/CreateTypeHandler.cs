
using Catalog.Application.Commands.Types;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Types
{
    public class CreateTypeHandler : IRequestHandler<CreateTypeCommand, TypeResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CreateTypeHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TypeResponse> Handle(CreateTypeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Payload.Name))
            {
                throw new ArgumentException("Name not provided.");
            }

            var type = CatalogMapper.Mapper.Map<ProductType>(request.Payload);

            await _unitOfWork.Repository<ProductType>().AddAsync(type, cancellationToken);

            var response = CatalogMapper.Mapper.Map<TypeResponse>(type);

            return response;
        }
    }
}
