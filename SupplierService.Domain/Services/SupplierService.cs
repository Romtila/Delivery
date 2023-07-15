using Delivery.BaseLib.Common.Enums;
using Delivery.BaseLib.Domain.Exceptions;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Entities.Enums;
using SupplierService.Domain.Exceptions;
using SupplierService.Domain.Repositories;
using SupplierService.Domain.Services.Interfaces;

namespace SupplierService.Domain.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierOrderItemRepository _orderItemRepository;
    private readonly ISupplierRepository _repository;

    public SupplierService(ISupplierRepository repository, ISupplierOrderItemRepository orderItemRepository)
    {
        _repository = repository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<SupplierOrder> Validate(long id, CancellationToken ct)
    {
        var kitchenOrder = await _repository.FindAsync(id, ct);

        if (kitchenOrder is null)
            throw new SupplierOrderNotFoundException();

        return kitchenOrder;
    }

    public async Task<SupplierOrder> FinishOrder(long id, CancellationToken ct)
    {
        var kitchenOrder = await Validate(id, ct);

        ValidateUpdatingOrder(kitchenOrder);

        kitchenOrder.Status = SupplierOrderStatus.Finished;
        await _repository.UpdateAsync(kitchenOrder, ct);

        return kitchenOrder;
    }

    public async Task<SupplierOrder> CancelOrder(long id, CancellationToken ct)
    {
        var kitchenOrder = await Validate(id, ct);

        ValidateUpdatingOrder(kitchenOrder);

        kitchenOrder.Status = SupplierOrderStatus.Cancelled;
        await _repository.UpdateAsync(kitchenOrder, ct);

        return kitchenOrder;
    }

    public async Task HandleNewOrder(SupplierOrder entity, CancellationToken ct)
    {
        await _repository.AddAsync(entity, ct);
        await _orderItemRepository.AddAsync(entity.Items, ct);
    }

    private static void ValidateUpdatingOrder(SupplierOrder order)
    {
        if (order.Status == SupplierOrderStatus.Preparing)
            return;

        throw new BusinessLogicException(
            $"It is not possible update this order. Reason: Order status is {order.Status.GetDescription(true)}.");
    }
}