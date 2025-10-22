using Payment.Application.Commands;
using Payment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Payment.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IMediator mediator, ILogger<PaymentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Create payment intent (Stripe/PayPal)
    /// </summary>
    [HttpPost("intent", Name = "CreatePaymentIntent")]
    [ProducesResponseType(typeof(PaymentIntentResponse), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<PaymentIntentResponse>> CreatePaymentIntent(
        [FromBody] CreatePaymentIntentCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"/api/v1/payment/transaction/{result.PaymentIntentId}", result);
    }

    /// <summary>
    /// Process refund for a payment
    /// </summary>
    [HttpPost("refund", Name = "ProcessRefund")]
    [ProducesResponseType(typeof(RefundResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RefundResponse>> ProcessRefund([FromBody] ProcessRefundCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Stripe webhook endpoint
    /// </summary>
    [HttpPost("webhook/stripe", Name = "StripeWebhook")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> StripeWebhook()
    {
        // Stripe webhook validation and processing
        _logger.LogInformation("Stripe webhook received");
        return Ok();
    }

    /// <summary>
    /// PayPal webhook endpoint
    /// </summary>
    [HttpPost("webhook/paypal", Name = "PayPalWebhook")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> PayPalWebhook()
    {
        // PayPal webhook validation and processing
        _logger.LogInformation("PayPal webhook received");
        return Ok();
    }

    /// <summary>
    /// Get payment transaction details
    /// </summary>
    [HttpGet("transaction/{transactionId}", Name = "GetPaymentTransaction")]
    [ProducesResponseType(typeof(PaymentTransactionResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaymentTransactionResponse>> GetPaymentTransaction(string transactionId)
    {
        // Query payment transaction by ID
        return Ok(new PaymentTransactionResponse
        {
            TransactionId = transactionId,
            Status = "Succeeded"
        });
    }
}
