using Catalog.API.Constants;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Commands.Products;
using Catalog.Application.Commands.Types;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Queries.Products;
using Catalog.Application.Queries.Types;
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
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        [Route("GetProductsByBrandId")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        [Route("GetProductsByName")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        /// <param name="typeId">Product Type.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProductsByTypeId")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] { NameConstants.ProductQuerySwaggerName })]
        public async Task<ActionResult> GetProductsByTypeId(
            [FromQuery]string typeId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 15)
        {
            var result = await _mediator.Send(new GetProductsByTypeQuery
            {
                TypeId = typeId,
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
        [Route("SearchProductsByName")]
        [ProducesResponseType(typeof(List<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.ProductCommandSwaggerName})]
        public async Task<ActionResult> UpdateProduct([FromBody]UpdateProductRequest payload)
        {
            var result = await _mediator.Send(new UpdateProductCommand
            {
                Payload = payload
            });

            return Ok(result);
        }

        /// <summary>
        /// Removes the provided Product from the system.
        /// </summary>
        /// <param name="Id">Product Id.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{Id}/", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
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

        #region "Brand Query"

        /// <summary>
        /// Gets all the Product Brands in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(List<BrandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BrandQuerySwaggerName})]
        public async Task<ActionResult> GetAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrandsQuery());

            return Ok(result);
        }

        #endregion

        #region "Brand Command"

        /// <summary>
        /// Creates new Brand in the system.
        /// </summary>
        /// <param name="payload">Product Brand Request Data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateBrand")]
        [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BrandCommandSwaggerName})]
        public async Task<ActionResult> CreateBrand([FromBody]CreateBrandRequest payload)
        {
            var result = await _mediator.Send(new CreateBrandCommand
            {
                Payload = payload
            });

            return Ok(result);
        }

        /// <summary>
        /// Updates the given Product Brand in the system.
        /// </summary>
        /// <param name="payload">Modified Product Brand Data.</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdateBrand")]
        [ProducesResponseType(typeof(BrandResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BrandCommandSwaggerName})]
        public async Task<ActionResult> UpdateBrand([FromBody]UpdateBrandRequest payload)
        {
            var result = await _mediator.Send(new UpdateBrandCommand
            {
                Payload = payload
            });

            return Ok(result);
        }

        /// <summary>
        /// Removes the given Product Brand from the system.
        /// </summary>
        /// <param name="id">Brand Id.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteBrand")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.BrandCommandSwaggerName})]
        public async Task<ActionResult> DeleteBrand(string id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand
            {
                BrandId = id
            });

            return Ok(result);
        }

        #endregion

        #region "Type Query"


        /// <summary>
        /// Gets all Product Types in the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(List<TypeResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.TypeQuerySwaggerName})]
        public async Task<ActionResult> GetAllTypes()
        {
            var result = await _mediator.Send(new GetAllProductTypesQuery());

            return Ok(result);
        }

        #endregion

        #region "Type Command"

        /// <summary>
        /// Creates a new Producte Type in the system.
        /// </summary>
        /// <param name="payload">New Product Type request data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateType")]
        [ProducesResponseType(typeof(TypeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.TypeCommandSwaggerName})]
        public async Task<ActionResult> CreateType([FromBody] CreateTypeRequest payload)
        {
            var result = await _mediator.Send(new CreateTypeCommand
            {
                Payload = payload
            });

            return Ok(result);
        }


        /// <summary>
        /// Updates the given Product Type in the system.
        /// </summary>
        /// <param name="payload">The updated Product Type.</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdateType")]
        [ProducesResponseType(typeof(TypeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.TypeCommandSwaggerName})]
        public async Task<ActionResult> UpdateType([FromBody] UpdateTypeRequest payload)
        {
            var result = await _mediator.Send(new UpdateTypeCommand
            {
                Payload = payload
            });

            return Ok(result);
        }


        /// <summary>
        /// Removes the given Product Type from the system.
        /// </summary>
        /// <param name="id">Type Id.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}", Name = "DeleteType")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerOperation(Tags = new[] {NameConstants.TypeCommandSwaggerName})]
        public async Task<ActionResult> DeleteType(string id)
        {
            var result = await _mediator.Send(new DeleteTypeCommand
            {
                TypeId = id
            });

            return Ok(result);
        }

        #endregion

    }
}
