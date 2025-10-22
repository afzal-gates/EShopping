namespace Supplier.Application.Responses;

public class SupplierResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public bool IsActive { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedDate { get; set; }
}
