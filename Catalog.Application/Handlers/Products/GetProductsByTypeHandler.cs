using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Handlers.Products
{
    public class GetProductsByTypeHandler : IRequestHandler<GetProductsByTypeQuery, IList<ProductResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetProductsByTypeHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex < 1)
                request.PageIndex = 1;

            if (request.PageSize < 1)
                request.PageSize = 15;

            if (request.PageSize > 100)
                request.PageSize = 100;

            var productList = await Task.FromResult(_unitOfWork.Repository<Product>().Read().Where(x => x.ProductTypeId == new ObjectId(request.TypeId))
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList());
            var productResponseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
