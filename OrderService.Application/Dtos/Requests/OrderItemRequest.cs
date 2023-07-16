using System.ComponentModel.DataAnnotations;
using Delivery.BaseLib.Core.DataAnnotations;

namespace OrderService.Application.Dtos.Requests;

public class OrderItemRequest
{
    [MinLength(3)] [Required] public string Name { get; set; } = string.Empty;

    [Required] [Min(0.01)] public decimal Price { get; set; }

    [Min(1)] [Required] public int Quantity { get; set; }
}