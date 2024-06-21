
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IList<ProductResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetAllProductsHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productlist = _unitOfWork.Repository<Product>().GetAllAsync();
            var productResponseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productlist);
            return productResponseList;
        }
    }
}
