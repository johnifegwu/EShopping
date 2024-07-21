using Basket.API.Constants;
using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Application.RpcClients;
using Basket.Core.Entities;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.API.Controllers
{
    /// <summary>
    /// A controller for handling all Shopping Cart related Commands and Queries.
    /// </summary>
    public class BasketController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly DiscountProtoServiceClient _client;

        public BasketController(IMediator mediator,  DiscountProtoServiceClient client)
        {
            this._mediator = mediator;
            this._client = client;
        }


        #region "Queries"


        /// <summary>
        /// Gets all the shopping cart items for the given user.
        /// </summary>
        /// <param name="username">Current user.</param>
        /// <returns>ShoppingCartResponse</returns>
        [HttpGet]
        [Route("[action]/{username}", Name = "GetShoppingCartByName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BasketQuerySwaggerName})]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> GetShoppingCartByName(string username)
        {
            var result = await _mediator.Send(new GetBasketByUserNameQuery
            {
                UserName = username
            });

            return Ok(result);
        }

        #endregion

        #region "Commands"

        /// <summary>
        /// Adds the provided shopping cart item to the users shopping cart if it does not exist, or
        /// Updates the users Shopping cart with the provided item if the quantity is greater than zero.
        /// or removes the given item from the shopping cart if the quantity is less than one.
        /// </summary>
        /// <param name="username">Current user.</param>
        /// <param name="cartItem" cref="ShoppingCartItem">Shopping Cart Item.</param>
        /// <returns cref="ShoppingCartResponse">ShoppingCartResponse</returns>
        [HttpPost]
        [Route("[action]/{username}", Name = "AddUpdateDeleteShoppingCartItem")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BasketCommandSwaggerName})]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> AddUpdateDeleteShoppingCartItem(
            string username,
            [FromBody]ShoppingCartItem cartItem)
        {
            var result = await _mediator.Send(new AddUpdateDeleteShoppingCartItemCommand
            {
                UserName = username,
                ShoppingCartItem = cartItem
            });

            return Ok(result);
        }


        /// <summary>
        /// Create a new shopping cart in the system for the given user 
        /// or updates it if one already exist.
        /// </summary>
        /// <param name="username">Current User.</param>
        /// <param name="cart">Shopping Cart object.</param>
        /// <returns cref="ShoppingCartResponse>ShoppingCartResponse</returns>
        [HttpPost]
        [Route("[action]/{username}", Name = "CreateOrUpdateShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BasketCommandSwaggerName})]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> CreateOrUpdateShoppingCart(
            string username, 
            [FromBody]ShoppingCart cart)
        {
            var result = await _mediator.Send(new CreateOrUpdateShoppingCartCommand
            {
                UserName = username,
                ShoppingCart = cart
            });

            return Ok(result);
        }

        /// <summary>
        /// Removes the basket that belongs to the provided user from the system if it exist.
        /// </summary>
        /// <param name="username">Current user.</param>
        /// <returns>bool</returns>
        [HttpDelete]
        [Route("[action]/{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BasketCommandSwaggerName})]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            var result = await _mediator.Send(new DeleteBasketByUserNameCommand
            {
                UserName = username
            });

            return Ok(result);
        }

        #endregion

        #region "Coupon Query"

        /// <summary>
        /// Gets coupon by product Id from the system.
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>DiscountModel</returns>
        [HttpGet]
        [Route("[action]/{productId}", Name = "GetCouponByProductId")]
        [ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.DiscountQuerySwaggerName })]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> GetCouponByProductId(string productId)
        {
            var result = await DiscountRpcClient.GetDiscountAsync(productId, _client);
            return Ok(result);
        }

        #endregion

        #region "Coupon Command"

        /// <summary>
        /// Creates a coupon in the system.
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns>DiscountModel</returns>
        [HttpPost]
        [Route("CreateCoupon")]
        [ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.DiscountCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateCoupon([FromBody] CreateDiscountModel payload)
        {
            var result = await DiscountRpcClient.CreateDiscountAsync(payload, _client);
            return Ok(result);
        }

        /// <summary>
        /// Updates a coupon in the system.
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns>DiscountModel</returns>
        [HttpPatch]
        [Route("UpdateCoupon")]
        [ProducesResponseType(typeof(DiscountModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.DiscountCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCoupon([FromBody] DiscountModel payload)
        {
            var result = await DiscountRpcClient.UpdateDiscountAsync(payload, _client);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a coupon from the system.
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>bool</returns>
        [HttpDelete]
        [Route("[action]/{productId}", Name = "DeleteCoupon")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.DiscountCommandSwaggerName })]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCoupon(string productId)
        {
            var result = await DiscountRpcClient.DeleteDiscountAsync(productId, _client);
            return Ok(result);
        }

        #endregion
    }
}
