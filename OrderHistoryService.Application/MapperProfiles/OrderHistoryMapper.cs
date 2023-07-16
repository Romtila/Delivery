using Delivery.BaseLib.Common.Enums;
using OrderHistoryService.Application.Dtos.Responses;
using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Entities.Enums;
using OrderHistoryService.Domain.Events.OrderService;
using Riok.Mapperly.Abstractions;

namespace OrderHistoryService.Application.MapperProfiles;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByValue, EnumMappingIgnoreCase = true)]
public static partial class OrderHistoryMapper
{
    public static partial OrderHistoryStatus EnumValueToOrderHistoryStatus(this EnumValue enumValue);

    public static partial OrderHistoryResponse OrderHistoryToOrderHistoryResponse(this OrderHistory orderHistory);
    public static partial IEnumerable<OrderHistoryResponse> OrderHistoriesToOrderHistoryResponses(this IEnumerable<OrderHistory> orderHistories);
    public static partial OrderItemHistoryResponse OrderItemHistoryToOrderItemHistoryResponse(this OrderItemHistory orderItemHistory);

    public static partial OrderHistory OrderCreatedEventToOrderHistory(this OrderCreatedEvent orderCreatedEvent);


    [MapProperty(nameof(OrderItemCreatedEvent.Id), nameof(OrderItemHistory.OrderItemId))]
    [MapperIgnoreTarget(nameof(OrderItemHistory.Id))]
    public static partial OrderItemHistory OrderItemCreatedEventToOrderItemHistory(this OrderItemCreatedEvent orderItemCreatedEvent);
}