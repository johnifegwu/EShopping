
using Data.Repositories;
using Discount.Application.Commands;
using Discount.Core.Entities;
using eShopping.Exceptions;
using Grpc.Core;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteCouponHandler : IRequestHandler<DeleteCouponCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteCouponHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await Task.FromResult(_unitOfWork.Repository<Coupon>().Get().Where(x => x.ProductId == request.ProductId).FirstOrDefault());
            
            if (coupon != null)
            {
                await _unitOfWork.Repository<Coupon>().DeleteAsync(coupon, cancellationToken);

                return true;
            }

            throw new RpcException(new Status(StatusCode.NotFound, $"Discount {request.ProductId} not found"));
        }
    }
}
