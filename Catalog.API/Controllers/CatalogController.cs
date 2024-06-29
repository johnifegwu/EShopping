using Catalog.API.Constants;
using Catalog.Application.Commands.Products;
using Catalog.Application.Queries.Products;
using Catalog.Application.Requests;
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

        #region "Product Query"

        /// <summary>
        /// Gets all the Products from the system.
        /// </summary>
        /// <param name="pageIndex">Page index (default is 1).</param>
        /// <param name="pageSize">Page size (default is 15).</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "GetAllProducts")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] { NameConstants.ProductQuerySwaggerName })]
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
        [SwaggerOperation(Tags = new[] { NameConstants.ProductQuerySwaggerName })]
        public async Task<ActionResult> GetProductById(string Id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery 
            { 
                Id = Id 
            });

            return Ok(result);
        }


        /// <summary>
        /// Gets Products from the system by brandId.
        /// </summary>
        /// <param name="brandId">Product Brand Id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "GetProductsByBrandId")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] { NameConstants.ProductQuerySwaggerName })]
        public async Task<ActionResult> GetProductsByBrandId(
            [FromQuery]string brandId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _mediator.Send(new GetProductsByBrandQuery
            {
                BrandId = brandId,
                PageSize = pageSize,
                PageIndex = pageIndex
            });

            return Ok(result);
        }

        /// <summary>
        /// Gets Product(s) from the system by name.
        /// </summary>
        /// <param name="name">Product Name.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "GetProductsByName")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductQuerySwaggerName})]
        public async Task<ActionResult> GetProductsByName(
            [FromQuery]string name,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _mediator.Send(new GetProductsByNameQuery
            {
                ProductName = name,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(result);
        }

        /// <summary>
        /// Gets Products from the system by product type.
        /// </summary>
        /// <param name="type">Product Type.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "GetProductsByType")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] { NameConstants.ProductQuerySwaggerName })]
        public async Task<ActionResult> GetProductsByType(
            [FromQuery]string type,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _mediator.Send(new GetProductsByTypeQuery
            {
                TypeId = type,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(result);
        }


        /// <summary>
        /// Search Products by name-part.
        /// </summary>
        /// <param name="namePart">Search Term.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/", Name = "SearchProductsByName")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductQuerySwaggerName})]
        public async Task<ActionResult> SearchProductsByName(
            [FromQuery]string namePart,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _mediator.Send(new SearchProductsByNameQuery
            {
                ProductName = namePart,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(result);  
        }

        #endregion

        #region "Product Command"

        /// <summary>
        /// Creates a new Product in the system.
        /// </summary>
        /// <param name="payload">Create Product request payload.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]/", Name = "CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductCommandSwaggerName})]
        public async Task<ActionResult> CreateProduct([FromBody]CreateProductRequest payload)
        {
            var result = await _mediator.Send(new CreateProductCommand
            {
                Payload = payload
            });

            return Ok(result);
        }

        /// <summary>
        /// Updates the given Product in the system.
        /// </summary>
        /// <param name="payload">Update Product request payload.</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("[action]/", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductCommandSwaggerName})]
        public async Task<ActionResult> UpdateProduct([FromBody]UpdateProductRequest payload)
        {
            var result = await _mediator.Send(new UpdateProductCommand
            {
                Payload = payload
            });

            return Ok(result);
        }

        [HttpDelete]
        [Route("[action]/{Id}/", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductCommandSwaggerName})]
        public async Task<ActionResult> DeleteProduct(string Id)
        {
            var result = await _mediator.Send(new DeleteProductCommand
            {
                ProductId = Id
            });

            return Ok(result);
        }

        #endregion

    }
}
