using System.ComponentModel.DataAnnotations;
using Delivery.BaseLib.Core.DataAnnotations;

namespace OrderService.Application.Dtos.Requests;

public class OrderRequest
{
    [Min(1)] [Required] public long CustomerId { get; set; }

    [MinLength(1)] [Required] public List<OrderItemRequest> Items { get; set; } = new();
}