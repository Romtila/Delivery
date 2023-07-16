using Delivery.BaseLib.Common.Enums;
using DeliveryService.Application.Dtos.Responses;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Entities.Enums;
using Riok.Mapperly.Abstractions;

namespace DeliveryService.Application.MapperProfiles;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByValue, EnumMappingIgnoreCase = true)]
public static partial class DeliveryOrderMapper
{
    public static partial DeliveryOrderStatus EnumValueToDeliveryOrderStatus(this EnumValue enumValue);

    public static partial DeliveryOrderResponse DeliveryOrderToDeliveryOrderResponse(this DeliveryOrder deliveryOrder);
    public static partial IEnumerable<DeliveryOrderResponse> DeliveryOrdersToDeliveryOrderResponses(this IEnumerable<DeliveryOrder> deliveryOrders);
}