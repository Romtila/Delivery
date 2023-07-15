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

    public async Task HandleOrderCancelledEvent(long orderId, CancellationToken ct)
    {
        var order = await Validate(orderId, ct);

        order.Status = OrderHistoryStatus.Cancelled;
        await UpdateAndCommitOrder(order, ct);
    }

    public async Task HandleOrderCompletedEvent(long orderId, CancellationToken ct)
    {
        var order = await Validate(orderId, ct);

        order.Status = OrderHistoryStatus.Completed;
        order.DeliveredAt = DateTime.Now;
        await UpdateAndCommitOrder(order, ct);
    }

    public async Task HandleNewOrder(OrderHistory entity, CancellationToken ct)
    {
        entity.CreatedAt = DateTime.Now;
        entity.Status = OrderHistoryStatus.Preparing;
        await _repository.AddAsync(entity, ct);
        await _orderItemHistoryRepository.AddAsync(entity.Items, ct);
    }

    public async Task HandleDeliveryStartedEvent(DeliveryStartedEvent contextMessage, CancellationToken ct)
    {
        var order = await Validate(contextMessage.OrderId, ct);

        order.Status = OrderHistoryStatus.Delivering;
        order.DeliveryAddress = contextMessage.DeliveryAddress;
        await UpdateAndCommitOrder(order, ct);
    }

    public async Task HandleKitchenFinishedEvent(SupplierFinishedEvent contextMessage, CancellationToken ct)
    {
        var order = await Validate(contextMessage.OrderId, ct);

        order.Status = OrderHistoryStatus.AwaitingPickup;
        await UpdateAndCommitOrder(order, ct);
    }

    private async Task UpdateAndCommitOrder(OrderHistory order, CancellationToken ct)
    {
        await _repository.UpdateAsync(order, ct);
    }

    private async Task<OrderHistory> Validate(long id, CancellationToken ct)
    {
        var orderHistory = await _repository.FindAsync(id, ct);

        if (orderHistory is null)
            throw new OrderHistoryNotFoundException();

        return orderHistory;
    }
}