using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Events.SupplierService;

namespace OrderHistoryService.Domain.Services.Interfaces;

public interface IOrderHistoryService
{
    void HandleOrderCancelledEvent(long orderId);
    void HandleOrderCompletedEvent(long orderId);
    void HandleNewOrder(OrderHistory entity);
    void HandleDeliveryStartedEvent(DeliveryStartedEvent contextMessage);
    void HandleSupplierFinishedEvent(SupplierFinishedEvent contextMessage);
}