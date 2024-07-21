using MediatR;
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


        #endregion

    }
}
