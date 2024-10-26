using System.Net;
using System.Threading;
using Catalog.Application.Caching;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using Common.Logging;
using Common.Logging.Correlation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger<CatalogController> _logger;
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public CatalogController(IMediator mediator, ILogger<CatalogController> logger, ICorrelationIdGenerator correlationIdGenerator)
    {
        _mediator = mediator;
        _logger = logger;
        _correlationIdGenerator = correlationIdGenerator;
        _logger.LogInformation("CorrelationId {correlationId}:", _correlationIdGenerator.Get());
    }

    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"product_{id}";
        var response = await cacheService.GetDataAsync<ProductResponse>(
            cacheKey,
            cancellationToken);

        if (response is not null)
        {
            return Ok(response);
        }

        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);

        await cacheService.SetDataAsync<ProductResponse>(
            cacheKey,
            result,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductByProductName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts(
        [FromQuery] CatalogSpecParams catalogSpecParams,
        IHttpContextAccessor httpContextAccessor,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        try
        {
            var cacheKey = await cacheService.GenerateCacheKeyFromRequest(httpContextAccessor.HttpContext.Request);
            var response = await cacheService.GetDataAsync<Pagination<ProductResponse>>(
                cacheKey,
                cancellationToken);

            if (response is not null)
            {
                return Ok(response);
            }

            var query = new GetAllProductsQuery(catalogSpecParams);
            var result = await _mediator.Send(query);

            await cacheService.SetDataAsync<Pagination<ProductResponse>>(
            cacheKey,
            result,
            cancellationToken);

            _logger.LogInformation("All products retrieved");
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An Exception has occured: {Exception}");
            throw;
        }
    }

    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands(
        IHttpContextAccessor httpContextAccessor,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var cacheKey = await cacheService.GenerateCacheKeyFromRequest(httpContextAccessor.HttpContext.Request);
        var response = await cacheService.GetDataAsync<IList<BrandResponse>>(
            cacheKey,
            cancellationToken);

        if (response is not null)
        {
            return Ok(response);
        }

        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);

        await cacheService.SetDataAsync<IList<BrandResponse>>(
           cacheKey,
           result,
           cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypesResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<TypesResponse>>> GetAllTypes(
        IHttpContextAccessor httpContextAccessor,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var cacheKey = await cacheService.GenerateCacheKeyFromRequest(httpContextAccessor.HttpContext.Request);
        var response = await cacheService.GetDataAsync<IList<TypesResponse>>(
            cacheKey,
            cancellationToken);

        if (response is not null)
        {
            return Ok(response);
        }

        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);

        await cacheService.SetDataAsync<IList<TypesResponse>>(
           cacheKey,
           result,
           cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{brand}", Name = "GetProductsByBrandName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductsByBrandName(string brand)
    {
        var query = new GetProductByBrandQuery(brand);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponse>> CreateProduct(
        [FromBody] CreateProductCommand productCommand,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(productCommand);

        var cacheKey = $"product_{result.Id}";
        await cacheService.SetDataAsync<ProductResponse>(
           cacheKey,
           result,
           cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand productCommand,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(productCommand);

        var cacheKey = $"product_{productCommand.Id}";
        await cacheService.RemoveDataAsync(cacheKey, cancellationToken);
        var query = new GetProductByIdQuery(productCommand.Id);
        var product = await _mediator.Send(query);
        await cacheService.SetDataAsync<ProductResponse>(cacheKey, product, cancellationToken);

        return Ok(result);
    }
    [HttpDelete]
    [Route("{id}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProduct(string id,
        IRedisCacheService cacheService,
        CancellationToken cancellationToken)
    {
        var query = new DeleteProductByIdQuery(id);
        var result = await _mediator.Send(query);
        var cacheKey = $"product_{id}";
        await cacheService.RemoveDataAsync(cacheKey, cancellationToken);
        return Ok(result);
    }
}