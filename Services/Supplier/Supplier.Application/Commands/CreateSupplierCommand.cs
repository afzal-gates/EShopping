using MediatR;
using Supplier.Application.Responses;

namespace Supplier.Application.Commands;

public class CreateSupplierCommand : IRequest<SupplierResponse>
{
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string? PaymentTerms { get; set; }
    public string? CreatedBy { get; set; }
}
