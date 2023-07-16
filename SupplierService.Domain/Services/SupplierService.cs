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
        var supplierOrder = _repository.Find(id);

        if (supplierOrder is null)
            throw new SupplierOrderNotFoundException();

        return supplierOrder;
    }

    public SupplierOrder FinishOrder(long id)
    {
        var supplierOrder = Validate(id);

        ValidateUpdatingOrder(supplierOrder);

        supplierOrder.Status = SupplierOrderStatus.Finished;
        _repository.Update(supplierOrder);

        return supplierOrder;
    }

    public SupplierOrder CancelOrder(long id)
    {
        var supplierOrder = Validate(id);

        ValidateUpdatingOrder(supplierOrder);

        supplierOrder.Status = SupplierOrderStatus.Cancelled;
        _repository.Update(supplierOrder);

        return supplierOrder;
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