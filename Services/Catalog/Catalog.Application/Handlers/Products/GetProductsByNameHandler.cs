

using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class GetProductsByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        public GetProductsByNameHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await Task.FromResult(_unitOfWork.Repository<Product>().Read()
                .Where(x => x.Name.ToLower() == request.ProductName.ToLower())
                .Skip((request.PageIndex -1) * request.PageSize)
                .Take(request.PageSize).ToList());

            var responseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productList);

            return responseList;
        }
    }
}
