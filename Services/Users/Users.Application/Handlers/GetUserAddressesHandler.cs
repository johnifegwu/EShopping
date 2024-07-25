
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Users.Application.Extensions;
using Users.Application.Mappers;
using Users.Application.Queries;
using Users.Application.Responses;

namespace Users.Application.Handlers
{
    public class GetUserAddressesHandler : IRequestHandler<GetUserAddressesQuery, IList<UserAddressResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetUserAddressesHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IList<UserAddressResponse>> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.CurrentUser.UserName);

            if (user == null)
            {
                throw new NotAuthorizedException();
            }

            var addresses = await _unitOfWork.GetUserAddresses(user.Id, request.PageIndex, request.PageSize);

            return UsersMapper.Mapper.Map<List<UserAddressResponse>>(addresses);
        }
    }
}
