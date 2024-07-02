using Catalog.Application.Mappers;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Brands
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetAllBrandsHandler(IUnitOfWorkCore unitOfWorkCore)
        {
            _unitOfWork = unitOfWorkCore;
        }

        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await Task.FromResult(_unitOfWork.Repository<ProductBrand>().Read().OrderBy(x => x.Name).ToList());
            var brandResponseList = CatalogMapper.Mapper.Map<IList<BrandResponse>>(brandList);
            return brandResponseList;
        }
    }
}
