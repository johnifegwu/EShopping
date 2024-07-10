
using Data.Repositories;
using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Grpc.Protos;
using Discount.Core.Entities;
using eShopping.Exceptions;
using MediatR;
using Grpc.Core;

namespace Discount.Application.Handlers
{
    public class UpdateCouponHandler : IRequestHandler<UpdateCouponCommand, DiscountModel>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateCouponHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<DiscountModel> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await Task.FromResult(_unitOfWork.Repository<Coupon>().Get().Where(x => x.ProductId == request.Payload.ProductId).FirstOrDefault());
            
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount {request.Payload.ProductId} not found"));
            }

            //Update fileds
            coupon.ProductName = request.Payload.ProductName;
            coupon.Description = request.Payload.Description;
            coupon.Amount = (decimal)request.Payload.Amount;

            await _unitOfWork.Repository<Coupon>().UpdateAsync(coupon, cancellationToken);

            return DiscountMapper.Mapper.Map<DiscountModel>(coupon);
        }
    }
}
