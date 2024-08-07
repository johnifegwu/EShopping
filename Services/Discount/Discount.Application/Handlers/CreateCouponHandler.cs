﻿
using Data.Repositories;
using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Grpc.Protos;
using Discount.Core.Entities;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateCouponHandler : IRequestHandler<CreateCouponCommand, DiscountModel>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public CreateCouponHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<DiscountModel> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await Task.FromResult(_unitOfWork.Repository<Coupon>().Read().Where(x => x.ProductId == request.Payload.ProductId).FirstOrDefault());
            
            if (coupon == null)
            {
                coupon = DiscountMapper.Mapper.Map<Coupon>(request.Payload);

                await _unitOfWork.Repository<Coupon>().AddAsync(coupon, cancellationToken);
            }

            return DiscountMapper.Mapper.Map<DiscountModel>(coupon);
        }
    }
}
