using System.ComponentModel.DataAnnotations;

namespace CustomerService.Application.Dtos.Requests;

public class CustomerRequest
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Address { get; set; } = string.Empty;
}