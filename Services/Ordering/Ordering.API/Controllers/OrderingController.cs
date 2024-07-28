using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Constants;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using eShopping.Security;
using Ordering.Application.Requests;
using Ordering.Application.Commands;

namespace Ordering.API.Controllers
{
    /// <summary>
    /// A controller for handling all Product Order Commands and Queries.
    /// </summary>
    public class OrderingController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderingController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        #region "Ordering Query"


        /// <summary>
        /// Gets all the orders that belongs to the given user.
        /// </summary>
        /// <param name="userName">Current user.</param>
        /// <param name="pageindex">Page index.</param>
        /// <param name="pagesize">Page size.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{username}", Name = "GetOrdersByUsername")]
        [ProducesResponseType(typeof(List<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.OrderingQuerySwaggerName })]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult> GetOrdersByUserName(
            string userName,
            [FromQuery]int pageindex,
            [FromQuery]int pagesize)
        {

            var result = await _mediator.Send(new GetOrdersByUserNameQuery
            {
                CurrentUser = User.GetUserClaims(),
                UserName = userName,
                PageIndex = pageindex,
                PageSize = pagesize
            });

            return Ok(result);
        }

        /// <summary>
        /// Gets the given order from the system by order guid.
        /// </summary>
        /// <param name="orderguid">Order Guid.</param>
        /// <returns cref="OrderResponse">OrderResponse object.</returns>
        [HttpGet]
        [Route("[action]/{orderguid}", Name = "GetOrdersByOrderGuid")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.OrderingQuerySwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetOrdersByOrderGuid(
            string orderguid)
        {

            var result = await _mediator.Send(new GetOrderByGuidQuery
            {
                CurrentUser = User.GetUserClaims(),
                OrderGuid = orderguid
            });

            return Ok(result);
        }

        /// <summary>
        /// Gets Orders from the system according to the provided search parameters.
        /// </summary>
        /// <param name="optionalUsername">Username (optional).</param>
        /// <param name="isPaid">If the order has been paid for or not.</param>
        /// <param name="isshipped">If the order is shipped or not.</param>
        /// <param name="isCanceled">If the order has been canceled or not.</param>
        /// <param name="isDeleted">If the order has been marked as deleted or not.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pagesize">Page size.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrdersByFlags")]
        [ProducesResponseType(typeof(List<OrderResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.OrderingQuerySwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetOrdersByFlags(
            [FromQuery]string optionalUsername,
            [FromQuery]bool isPaid,
            [FromQuery]bool isshipped,
            [FromQuery]bool isCanceled,
            [FromQuery]bool isDeleted,
            [FromQuery]int pageIndex,
            [FromQuery]int pagesize)
        {
            var result = await _mediator.Send(new GetOrdersByFlagsQuery
            {
                OptionalUserName = optionalUsername,
                IsPaid = isPaid,
                IsCanceled = isCanceled,
                IsDeleted = isDeleted,
                IsShipped = isshipped,
                PageIndex = pageIndex,
                PageSize = pagesize
            });

            return Ok(result);
        }

        #endregion

        #region "Ordering Commands"

        /// <summary>
        /// Creates the provided order for the given customer in the system.
        /// </summary>
        /// <param name="payload" cref="CreateOrderRequest">Create Order Request object.</param>
        /// <returns cref="OrderResponse">Order Response object.</returns>
        [HttpPost]
        [Route("CreateCustomerOrder")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.OrderingQuerySwaggerName })]
        [Authorize(Roles = "Customer, Admin")]
        public async Task<ActionResult> CreateCustomerOrder([FromBody] CreateOrderRequest payload)
        {
            var result = await _mediator.Send(new CreateOrderCommand
            {
                CurrentUser = User.GetUserClaims(),
                Payload = payload
            });

            return Ok(result);
        }

        #endregion
    }
}
