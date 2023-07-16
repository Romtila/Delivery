using OrderService.Application.Dtos.Requests;
using OrderService.Application.Dtos.Responses;
using OrderService.Domain.Commands;
using OrderService.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace OrderService.Application.MapperProfiles;

[Mapper]
public static partial class OrderMapper
{
    public static partial OrderResponse OrderToOrderResponse(this Order order);
    public static partial OrderItemResponse OrderItemToOrderItemResponse(this OrderItem orderItem);
    public static partial CreateNewOrderItemCommand OrderItemRequestToCreateNewOrderItemCommand(this OrderItemRequest orderItemRequest);
    public static partial CreateNewOrderCommand OrderRequestToCreateNewOrderCommand(this OrderRequest orderRequest);

    // CreateMap<OrderStatus, EnumValue>().ConvertUsing(src => src.GetValue());
}