using System.ComponentModel.DataAnnotations;

namespace CustomerService.Application.Dtos.Requests;

public class ChargeCustomerRequest
{
    [Required] public long CustomerId { get; set; }

    [Required] public decimal OrderPrice { get; set; }
}