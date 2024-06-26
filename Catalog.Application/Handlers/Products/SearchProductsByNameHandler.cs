

using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
{
    public class SearchProductsByNameHandler : IRequestHandler<SearchProductsByNameQuery, IList<ProductResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public SearchProductsByNameHandler(IUnitOfWorkCore unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ProductResponse>> Handle(SearchProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var productList = await Task.FromResult(_unitOfWork.Repository<Product>().Read()
                .Where(x => x.Name.ToLower().Contains(request.ProductName.ToLower()))
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList());

            var responseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productList);

            return responseList;
        }
    }
}
