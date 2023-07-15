using OrderService.Domain.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Services;

public interface IOrderService
{
    Task<Order> Validate(long id, CancellationToken ct);
    Task<Order> Add(CreateNewOrderCommand command, CancellationToken ct);

    Task HandleOrderCancelledEvent(long orderId, CancellationToken ct);
    // Task HandleOrderUpdatedEvent(long orderId, OrderStatus newStatus, CancellationToken ct);
}