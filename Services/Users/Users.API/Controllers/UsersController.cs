using eShopping.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IMediator mediator)
        {
            this._mediator = mediator;
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
        [ProducesResponseType(typeof(List<UserLoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersQuerySwaggerName })]
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
        [ProducesResponseType(typeof(List<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersQuerySwaggerName })]
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
        /// Creates a new Admin User with Adminand Customer roles in the system.
        /// </summary>
        /// <param name="payload">Create User Request object.</param>
        /// <returns cref="UserResponse">User Response object.</returns>
        [HttpPost]
        [Route("CreateAdminUser")]
        [ProducesResponseType(typeof(List<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerOperation(Tags = new[] { NameConstants.UsersQuerySwaggerName })]
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

        #endregion

    }
}
