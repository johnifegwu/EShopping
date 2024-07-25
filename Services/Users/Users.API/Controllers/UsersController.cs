using eShopping.MailMan.Interfaces;
using eShopping.MailMan.Models;
using eShopping.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Users.API.Constants;
using Users.Application.Commands;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.API.Controllers
{

    /// <summary>
    /// The controller for handling all user commands and queries.
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;

        public UsersController(IMediator mediator, IEmailService emailService)
        {
            this._mediator = mediator;
            this._emailService = emailService;
        }

        #region "Query"


        #endregion

        #region "Command"

        /// <summary>
        /// Authenticates the given user against the records in the system.
        /// </summary>
        /// <param name="payload">Username and Password.</param>
        /// <returns>UserLoginResponse object this include bearer token among other details..</returns>
        [HttpPost]
        [Route("AuthenticateUser")]
        [ProducesResponseType(typeof(UserLoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        public async Task<ActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest payload)
        {
            var restult = await _mediator.Send(new AuthenticateUserCommand
            {
                Payload = payload
            });

            return Ok(restult);
        }


        /// <summary>
        /// Creates a new user with Customer role in the system.
        /// </summary>
        /// <param name="payload">Create User Request object.</param>
        /// <returns cref="UserResponse">User Response object.</returns>
        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        public async Task<ActionResult> CreateUser([FromBody]NewUserRequest payload)
        {
            var result = await _mediator.Send(new CreateUserCommand
            {
                IsAdminUser = false,
                Payload = payload
            });

            return Ok(result);
        }

        /// <summary>
        /// Creates a new Admin User with Admin and Customer roles in the system.
        /// </summary>
        /// <param name="payload">Create User Request object.</param>
        /// <returns cref="UserResponse">User Response object.</returns>
        [HttpPost]
        [Route("CreateAdminUser")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAdminUser([FromBody] NewUserRequest payload)
        {
            var result = await _mediator.Send(new CreateUserCommand
            {
                CurrentUser = User.GetUserClaims(),
                IsAdminUser = false,
                Payload = payload
            });

            return Ok(result);
        }


        /// <summary>
        /// Adds the given address to a collection of addresses for the given user in the system.
        /// </summary>
        /// <param name="payload">Create address Request object.</param>
        /// <returns cref="UserAddressResponse">Address response object.</returns>
        [HttpPost]
        [Route("AddUserAddress")]
        [ProducesResponseType(typeof(UserAddressResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult> AddUserAddress([FromBody] CreateUserAddressRequest payload)
        {
            var result = await _mediator.Send(new AddUserAddressCommand
            {
                CurrentUser = User.GetUserClaims(),
                Payload = payload
            });
           
            return Ok(result);
        }


        /// <summary>
        /// Creates a time limited Guid for which is sent to the user email
        /// as part of the link which the user will use to initiate the password change.
        /// </summary>
        /// <param name="payload">Forgot Password Request object.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgotPassword")]
        [ProducesResponseType(typeof(ForgotPasswordResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordCommand payload)
        {
            var result = await _mediator.Send(payload);

            //Notify User via Email
            await _emailService.SendEmailAsync(payload.Email, "Forgot Password : eShopping", eShopping.Constants.NameConstants.ForgotEmailTemplate, new ForgotPasswordModel()
            {
                Email = payload.Email,
                UserName = result.UserName,
                Guid = result.GUID,
                ChangePasswordUrl = $"https://eshopping.com/security/changepwd-by-guid/{result.GUID}"
            });

            return Ok(result);
        }

        #endregion

    }
}
