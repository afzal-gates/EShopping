using Analytics.Application.Queries;
using Analytics.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Analytics.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IMediator mediator, ILogger<AnalyticsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get product profitability analysis
    /// </summary>
    [HttpGet("products/profitability", Name = "GetProductProfitability")]
    [ProducesResponseType(typeof(List<ProductProfitabilityResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<ProductProfitabilityResponse>>> GetProductProfitability(
        [FromQuery] string? productId,
        [FromQuery] string? sku,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string periodType = "Monthly",
        [FromQuery] int top = 100,
        [FromQuery] string orderBy = "GrossProfit",
        [FromQuery] bool descending = true)
    {
        var query = new GetProductProfitabilityQuery
        {
            ProductId = productId,
            SKU = sku,
            StartDate = startDate,
            EndDate = endDate,
            PeriodType = periodType,
            Top = top,
            OrderBy = orderBy,
            Descending = descending
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get order financial details
    /// </summary>
    [HttpGet("orders/financials", Name = "GetOrderFinancials")]
    [ProducesResponseType(typeof(List<OrderFinancialsResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<OrderFinancialsResponse>>> GetOrderFinancials(
        [FromQuery] int? orderId,
        [FromQuery] string? userName,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? orderStatus,
        [FromQuery] bool includeCancelled = false,
        [FromQuery] bool includeRefunded = true,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50)
    {
        var query = new GetOrderFinancialsQuery
        {
            OrderId = orderId,
            UserName = userName,
            StartDate = startDate,
            EndDate = endDate,
            OrderStatus = orderStatus,
            IncludeCancelled = includeCancelled,
            IncludeRefunded = includeRefunded,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get period summary (P&L statement)
    /// </summary>
    [HttpGet("period/summary", Name = "GetPeriodSummary")]
    [ProducesResponseType(typeof(List<PeriodSummaryResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<PeriodSummaryResponse>>> GetPeriodSummary(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string periodType = "Monthly",
        [FromQuery] int top = 12)
    {
        var query = new GetPeriodSummaryQuery
        {
            StartDate = startDate,
            EndDate = endDate,
            PeriodType = periodType,
            Top = top
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Get overall P&L dashboard summary
    /// </summary>
    [HttpGet("dashboard/summary", Name = "GetDashboardSummary")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<object>> GetDashboardSummary(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        startDate ??= DateTime.UtcNow.AddMonths(-1);
        endDate ??= DateTime.UtcNow;

        var periodSummaryQuery = new GetPeriodSummaryQuery
        {
            StartDate = startDate,
            EndDate = endDate,
            PeriodType = "Monthly",
            Top = 1
        };

        var productProfitQuery = new GetProductProfitabilityQuery
        {
            StartDate = startDate,
            EndDate = endDate,
            PeriodType = "Monthly",
            Top = 10,
            OrderBy = "GrossProfit",
            Descending = true
        };

        var periodSummary = await _mediator.Send(periodSummaryQuery);
        var topProducts = await _mediator.Send(productProfitQuery);

        var summary = new
        {
            Period = periodSummary.FirstOrDefault(),
            TopProducts = topProducts,
            DateRange = new { Start = startDate, End = endDate }
        };

        return Ok(summary);
    }
}
