
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Data.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
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
            var brandList = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            var brandResponseList = CatalogMapper.Mapper.Map<IList<BrandResponse>>(brandList);
            return brandResponseList;
        }
    }
}
