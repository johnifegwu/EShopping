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
            var productList = await Task.FromResult(_unitOfWork.Repository<Product>().Read().Where(x => x.ProductTypeId == new ObjectId(request.TypeId)).ToList());
            var productResponseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productList);
            return productResponseList;
        }
    }
}
