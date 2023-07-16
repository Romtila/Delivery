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

    public SupplierOrder Validate(long id)
    {
        var kitchenOrder = _repository.Find(id);

        if (kitchenOrder is null)
            throw new SupplierOrderNotFoundException();

        return kitchenOrder;
    }

    public SupplierOrder FinishOrder(long id)
    {
        var kitchenOrder = Validate(id);

        ValidateUpdatingOrder(kitchenOrder);

        kitchenOrder.Status = SupplierOrderStatus.Finished;
        _repository.Update(kitchenOrder);

        return kitchenOrder;
    }

    public SupplierOrder CancelOrder(long id)
    {
        var kitchenOrder = Validate(id);

        ValidateUpdatingOrder(kitchenOrder);

        kitchenOrder.Status = SupplierOrderStatus.Cancelled;
        _repository.Update(kitchenOrder);

        return kitchenOrder;
    }

    public void HandleNewOrder(SupplierOrder entity)
    {
        _repository.Add(entity);
        _orderItemRepository.Add(entity.Items);
        _repository.Commit();
    }

    private static void ValidateUpdatingOrder(SupplierOrder order)
    {
        if (order.Status == SupplierOrderStatus.Preparing)
            return;

        throw new BusinessLogicException(
            $"It is not possible update this order. Reason: Order status is {order.Status.GetDescription(true)}.");
    }
}