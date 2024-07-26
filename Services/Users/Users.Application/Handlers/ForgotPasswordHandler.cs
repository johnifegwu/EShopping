
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.MailMan.Interfaces;
using eShopping.Models;
using eShopping.Security;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<ForgotPasswordHandler> _logger;
        public ForgotPasswordHandler(IUnitOfWorkCore unitOfWork, IEmailService emailService, ILogger<ForgotPasswordHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._emailService = emailService;
            this._logger = logger;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            //Fluent Validation is failing for email and other regex
            //So we revalidate here just incase it fails.
            request.Email.ValidateEmail();

            var user = await _unitOfWork.GetUser(request.Email);

            if (user == null)
            {
                throw new NotFoundException("The email provided was not found in our records.");
            }

            //Generate GUID and update user.
            var guid = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddDays(1);

            user.PasswordRecoveryUID = guid;
            user.PasswordRecoveryUIDExpiry = expiryDate;

            await _unitOfWork.Repository<User>().UpdateAsync(user);


            try
            {
                //Notify User via Email
                await _emailService.SendEmailAsync(request.Email, "Forgot Password : eShopping", eShopping.Constants.NameConstants.ForgotEmailTemplate, new ForgotPasswordModel()
                {
                    Email = request.Email,
                    UserName = user.UserName,
                    Guid = guid,
                    ChangePasswordUrl = $"https://eshopping.com/security/changepwd-by-guid/{guid}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                return false;
            }

            return true;
        }
    }
}
