﻿using Catalog.Application.Mappers;
using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Products
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
            if (request.PageIndex < 1)
                request.PageIndex = 1;

            if (request.PageSize < 1)
                request.PageSize = 15;

            if (request.PageSize > 100)
                request.PageSize = 100;

            var productlist = await _unitOfWork.Repository<Product>().GetAllAsync(request.PageIndex, request.PageSize);
            var productResponseList = CatalogMapper.Mapper.Map<IList<ProductResponse>>(productlist);
            return productResponseList;
        }
    }
}
