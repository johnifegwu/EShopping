using eShopping.MailMan.Interfaces;
using eShopping.Models;
using eShopping.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Users.API.Constants;
using Users.Application.Commands;
using Users.Application.Queries;
using Users.Application.Requests;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.API.Controllers
{

    /// <summary>
    /// The controller for handling all user commands and queries.
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, IEmailService emailService, ILogger<UsersController> logger)
        {
            this._mediator = mediator;
            this._emailService = emailService;
            this._logger = logger;
        }

        #region "Query"

        /// <summary>
        /// Gets all the Address Types in the system.
        /// </summary>
        /// <returns cref="AddressTypeResponse">A list of Address Type Response object.</returns>
        [HttpGet]
        [Route("GetAddressTypes")]
        [ProducesResponseType(typeof(List<AddressTypeResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersQuerySwaggerName })]
        public async Task<ActionResult> GetAddressTypes()
        {
            var result = await _mediator.Send(new GetAddressTypesQuery());

            return Ok(result);
        }


        /// <summary>
        /// Gets all the addresses that belongs to the current user.
        /// </summary>
        /// <param name="pageIndex">Page index (default is 1).</param>
        /// <param name="pageSize">Page size (default is 5).</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserAddresses")]
        [ProducesResponseType(typeof(List<UserAddressResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersQuerySwaggerName })]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult> GetUserAddresses(
            [FromQuery]int pageIndex = 1,
            [FromQuery]int pageSize = 5)
        {
            var result = await _mediator.Send(new GetUserAddressesQuery
            {
                CurrentUser = User.GetUserClaims(),
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(result);
        }

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
                IsAdminUser = true, //Set to true add this user to Admin role
                Payload = payload
            });

            return Ok(result);
        }


        /// <summary>
        /// Add the given user to the admin role.
        /// </summary>
        /// <param name="username">User to be removed from admin role.</param>
        /// <returns>Boolean Value.</returns>
        [HttpPost]
        [Route("[action]/{username}", Name = "AddUserToAdminRole")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddUserToAdminRole(string username)
        {
            var result = await _mediator.Send(new AddUserToAdminRoleCommand
            {
                CurrentUser = User.GetUserClaims(),
                UserName = username
            });

            return Ok(result);
        }

        /// <summary>
        /// Removes the given user from the Admin role.
        /// </summary>
        /// <param name="username">User to be removed from admin role.</param>
        /// <returns>Boolean value.</returns>
        [HttpPatch]
        [Route("[action]/{username}",Name = "RemoveUserFromAdminRole")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveUserFromadminRole(string username)
        {
            var result = await _mediator.Send(new RemoveUserFromAdminRoleCommand
            {
                CurrentUser = User.GetUserClaims(),
                UserName = username
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
        /// Updates the provided modified address of the current user.
        /// </summary>
        /// <param name="payload">Modified address.</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdateUserAddress")]
        [ProducesResponseType(typeof(UserAddressResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult> UpdateUserAddress([FromBody] UpdateUserAddressRequest payload)
        {
            var result = await _mediator.Send(new UpdateUserAddressCommand
            {
                CurrentUser = User.GetUserClaims(),
                Payload = payload
            });

            return Ok(result);
        }


        /// <summary>
        /// Deletes the provided address of the current user from the system.
        /// </summary>
        /// <param name="id">Address Id.</param>
        /// <returns>Boolean Value.</returns>
        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteUserAddress")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult> DeleteUserAddress(int id)
        {
            var result = await _mediator.Send(new DeleteUserAddressCommand
            {
                CurrentUser = User.GetUserClaims(),
                AddressId = id
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
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordCommand payload)
        {
            var result = await _mediator.Send(payload);

            try
            {
                //Notify User via Email
                await _emailService.SendEmailAsync(payload.Email, "Forgot Password : eShopping", eShopping.Constants.NameConstants.ForgotEmailTemplate, new ForgotPasswordModel()
                {
                    Email = payload.Email,
                    UserName = result.UserName,
                    Guid = result.GUID,
                    ChangePasswordUrl = $"https://eshopping.com/security/changepwd-by-guid/{result.GUID}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(true);
        }


        /// <summary>
        /// Attempts to change the users password with the Guid sent to the users email previously.
        /// </summary>
        /// <param name="payload">Guid and Password</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("ChangePasswordByGUID")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        public async Task<ActionResult> ChangePasswordByGUID([FromBody] ChangePasswordByGUIDCommand payload)
        {
            var result = await _mediator.Send(payload);

            return Ok(result);
        }


        /// <summary>
        /// Attemps to change the password of the current user.
        /// </summary>
        /// <param name="payload">Old and New Passwords</param>
        /// <returns cref="UserLoginResponse">User Login Response object</returns>
        [HttpPatch]
        [Route("ChangePassword")]
        [ProducesResponseType(typeof(UserLoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersCommandSwaggerName })]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult> ChangePassword([FromBody]ChangePasswordCommand payload)
        {
            //Get current user's UserName.
            payload.UserName = User.GetUserClaims()?.UserName;

            var result = await _mediator.Send(payload);

            return Ok(result);
        }

        #endregion

    }
}
