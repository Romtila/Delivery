using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Entities.Enums;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Events.SupplierService;
using OrderHistoryService.Domain.Exceptions;
using OrderHistoryService.Domain.Repositories;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Domain.Services;

public class OrderHistoryService : IOrderHistoryService
{
    private readonly IOrderItemHistoryRepository _orderItemHistoryRepository;
    private readonly IOrderHistoryRepository _repository;

    public OrderHistoryService(IOrderHistoryRepository repository, IOrderItemHistoryRepository orderItemHistoryRepository)
    {
        _repository = repository;
        _orderItemHistoryRepository = orderItemHistoryRepository;
    }

    public void HandleOrderCancelledEvent(long orderId)
    {
        var order = Validate(orderId);

        order.Status = OrderHistoryStatus.Cancelled;

        UpdateAndCommitOrder(order);
    }

    public void HandleOrderCompletedEvent(long orderId)
    {
        var order = Validate(orderId);

        order.Status = OrderHistoryStatus.Completed;
        order.DeliveredAt = DateTime.Now;

        UpdateAndCommitOrder(order);
    }

    public void HandleNewOrder(OrderHistory entity)
    {
        entity.CreatedAt = DateTime.Now;
        entity.Status = OrderHistoryStatus.Preparing;

        _repository.Add(entity);
        _orderItemHistoryRepository.Add(entity.Items);
        _repository.Commit();
    }

    public void HandleDeliveryStartedEvent(DeliveryStartedEvent contextMessage)
    {
        var order = Validate(contextMessage.OrderId);

        order.Status = OrderHistoryStatus.Delivering;
        order.DeliveryAddress = contextMessage.DeliveryAddress;

        UpdateAndCommitOrder(order);
    }

    public void HandleKitchenFinishedEvent(SupplierFinishedEvent contextMessage)
    {
        var order = Validate(contextMessage.OrderId);

        order.Status = OrderHistoryStatus.AwaitingPickup;
        UpdateAndCommitOrder(order);
    }

    private OrderHistory Validate(long id)
    {
        var orderHistory = _repository.Find(id);

        if (orderHistory is null)
            throw new OrderHistoryNotFoundException();

        return orderHistory;
    }

    private void UpdateAndCommitOrder(OrderHistory order)
    {
        _repository.Update(order);
        _repository.Commit();
    }
}