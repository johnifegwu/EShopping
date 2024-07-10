
using Data.Repositories;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Application.Responses;
using Discount.Core.Entities;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetCouponByProductIdHandler : IRequestHandler<GetCouponByProductIdQuery, CouponResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetCouponByProductIdHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CouponResponse> Handle(GetCouponByProductIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await Task.FromResult(_unitOfWork.Repository<Coupon>().Read().Where(x => x.ProductId == request.ProductId).FirstOrDefault());

            return DiscountMapper.Mapper.Map<CouponResponse>(coupon);
        }
    }
}
