using System.ComponentModel.DataAnnotations;

namespace OrderHistoryService.Application.Dtos.Requests;

public class ChargeCustomerRequest
{
    [Required] public long CustomerId { get; set; }

    [Required] public decimal OrderPrice { get; set; }
}