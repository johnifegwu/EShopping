
using Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Mappers;
using Users.Application.Queries;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class GetAddressTypesHandler : IRequestHandler<GetAddressTypesQuery, IList<AddressTypeResponse>>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public GetAddressTypesHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IList<AddressTypeResponse>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<AddressType>().Read().ToListAsync();

            return UsersMapper.Mapper.Map<List<AddressTypeResponse>>(result);
        }
    }
}
