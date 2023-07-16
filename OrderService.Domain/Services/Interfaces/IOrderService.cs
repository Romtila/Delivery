using OrderService.Domain.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Services.Interfaces;

public interface IOrderService
{
    Order Validate(long id);
    Task<Order> Add(CreateNewOrderCommand command);

    Task HandleOrderCancelledEvent(long orderId);
    // Task HandleOrderUpdatedEvent(long orderId, OrderStatus newStatus, CancellationToken ct);
}