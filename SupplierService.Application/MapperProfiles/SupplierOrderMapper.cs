using Delivery.BaseLib.Common.Enums;
using Riok.Mapperly.Abstractions;
using SupplierService.Application.Dtos.Responses;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Entities.Enums;
using SupplierService.Domain.Events.OrderService;

namespace SupplierService.Application.MapperProfiles;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByValue, EnumMappingIgnoreCase = true)]
public static partial class SupplierOrderMapper
{
    public static partial SupplierOrderStatus EnumValueToSupplierOrderStatus(this EnumValue enumValue);
    
    public static partial SupplierOrderResponse SupplierOrderToSupplierOrderResponse(this SupplierOrder supplierOrder);
    public static partial IEnumerable<SupplierOrderResponse> SupplierOrdersToSupplierOrderResponses(this IEnumerable<SupplierOrder> supplierOrders);
    public static partial SupplierOrderItemResponse SupplierOrderItemToSupplierOrderItemResponse(this SupplierOrderItem supplierOrderItem);

    public static partial SupplierOrder OrderCreatedEventToSupplierOrder(this OrderCreatedEvent orderCreatedEvent);
    public static partial SupplierOrderItem OrderItemCreatedEventToSupplierOrderItem(this OrderItemCreatedEvent orderItemCreatedEvent);
}