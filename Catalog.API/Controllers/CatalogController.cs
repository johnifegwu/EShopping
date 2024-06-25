using Catalog.Application.Queries.Products;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Catalog.API.Controllers
{
    /// <summary>
    /// Product Catalog Controller.
    /// </summary>
    public class CatalogController : BaseController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Gets all the Products from the system.
        /// </summary>
        /// <param name="pageIndex">Page index (default is 1).</param>
        /// <param name="pageSize">Page size (default is 15).</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "GetAllProducts")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] { "eShopping Catalog | Products Query" })]
        public async Task<ActionResult> GetAllProducts(
            [FromQuery]int pageIndex = 1, 
            [FromQuery]int pageSize = 15)
        {
            var result = await _mediator.Send(new GetAllProductsQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            });
            return Ok(result);
        }


        /// <summary>
        /// Gets Product from the system by Id.
        /// </summary>
        /// <param name="Id">Id of the product to return.</param>
        /// <returns>ProductResponse</returns>
        [HttpGet]
        [Route("[action]/{Id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {"eShopping Catalog | Products Query"})]
        public async Task<ActionResult> GetProductById(string Id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery 
            { 
                Id = Id 
            });

            return Ok(result);
        }

    }
}
