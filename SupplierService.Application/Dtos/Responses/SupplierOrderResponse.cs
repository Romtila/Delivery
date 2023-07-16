using Delivery.BaseLib.Common.Enums;

namespace SupplierService.Application.Dtos.Responses;

public class SupplierOrderResponse
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public List<SupplierOrderItemResponse> Items { get; set; } = new();
    public EnumValue Status { get; set; } = new();
}