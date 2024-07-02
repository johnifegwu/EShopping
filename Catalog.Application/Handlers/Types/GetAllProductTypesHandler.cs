using Catalog.Application.Mappers;
using Catalog.Application.Queries.Types;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Types
{
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<TypeResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetAllProductTypesHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<TypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var typeList = await Task.FromResult(_unitOfWork.Repository<ProductType>().Read().OrderBy(x => x.Name).ToList());
            var typeResponseList = CatalogMapper.Mapper.Map<IList<TypeResponse>>(typeList);
            return typeResponseList;
        }
    }
}
