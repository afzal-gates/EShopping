using MediatR;
using Shipping.Application.Responses;

namespace Shipping.Application.Commands;

public class CreateShipmentCommand : IRequest<ShipmentResponse>
{
    public int OrderId { get; set; }
    public string CarrierCode { get; set; }
    public string ServiceType { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public string FromName { get; set; }
    public string FromAddressLine1 { get; set; }
    public string? FromAddressLine2 { get; set; }
    public string FromCity { get; set; }
    public string FromState { get; set; }
    public string FromZipCode { get; set; }
    public string FromCountry { get; set; }

    public string ToName { get; set; }
    public string ToAddressLine1 { get; set; }
    public string? ToAddressLine2 { get; set; }
    public string ToCity { get; set; }
    public string ToState { get; set; }
    public string ToZipCode { get; set; }
    public string ToCountry { get; set; }

    public string? CreatedBy { get; set; }
}
