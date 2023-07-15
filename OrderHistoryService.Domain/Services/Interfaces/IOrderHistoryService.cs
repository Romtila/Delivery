using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Events.SupplierService;

namespace OrderHistoryService.Domain.Services.Interfaces;

public interface IOrderHistoryService
{
    Task HandleOrderCancelledEvent(long orderId, CancellationToken ct);
    Task HandleOrderCompletedEvent(long orderId, CancellationToken ct);
    Task HandleNewOrder(OrderHistory entity, CancellationToken ct);
    Task HandleDeliveryStartedEvent(DeliveryStartedEvent contextMessage, CancellationToken ct);
    Task HandleKitchenFinishedEvent(SupplierFinishedEvent contextMessage, CancellationToken ct);
}