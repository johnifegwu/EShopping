

using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<TypesResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetAllProductTypesHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<TypesResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            var typeResponseList = CatalogMapper.Mapper.Map<IList<TypesResponse>>(typeList);
            return typeResponseList;
        }
    }
}
