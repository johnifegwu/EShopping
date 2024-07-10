﻿
using Data.Repositories;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Discount.Core.Entities;
using MediatR;
using Grpc.Core;

namespace Discount.Application.Handlers
{
    public class GetCouponByProductIdHandler : IRequestHandler<GetCouponByProductIdQuery, DiscountModel>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetCouponByProductIdHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<DiscountModel> Handle(GetCouponByProductIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await Task.FromResult(_unitOfWork.Repository<Coupon>().Read().Where(x => x.ProductId == request.ProductId).FirstOrDefault());

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount {request.ProductId} not found"));
            }

            return DiscountMapper.Mapper.Map<DiscountModel>(coupon);
        }
    }
}
