

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
            if (request.PageIndex < 1)
                request.PageIndex = 1;

            if (request.PageSize < 1)
                request.PageSize = 15;

            if (request.PageSize > 100)
                request.PageSize = 100;

            var productList = await Task.FromResult(_unitOfWork.Repository<Product>().Read()
                .Where(x => x.Name.Contains(request.ProductName))
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList());

            var responseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productList);

            return responseList;
        }
    }
}
