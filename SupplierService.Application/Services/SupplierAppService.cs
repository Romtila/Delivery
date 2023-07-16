using Delivery.BaseLib.Infrastructure.Transactions;
using MassTransit;
using SupplierService.Application.Dtos.Responses;
using SupplierService.Application.MapperProfiles;
using SupplierService.Application.Services.Interfaces;
using SupplierService.Domain.Events;
using SupplierService.Domain.Repositories;
using SupplierService.Domain.Services.Interfaces;

namespace SupplierService.Application.Services;

public class SupplierAppService : ISupplierAppService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly ISupplierService _supplierService;
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierAppService(IUnitOfWork unitOfWork, IPublishEndpoint publisher,
        ISupplierRepository supplierRepository, ISupplierService supplierService)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _supplierRepository = supplierRepository;
        _supplierService = supplierService;
    }

    public SupplierOrderResponse Get(long id)
    {
        var supplierOrder = _supplierService.Validate(id);

        return supplierOrder.SupplierOrderToSupplierOrderResponse();
    }

    public IEnumerable<SupplierOrderResponse> Get()
    {
        var orders = _supplierRepository.Query().ToList();
        return orders.SupplierOrdersToSupplierOrderResponses();
    }

    public async Task FinishOrder(long id)
    {
        var order = _supplierService.FinishOrder(id);
        _unitOfWork.Commit();

        await _publisher.Publish<SupplierFinishedEvent>(new
        {
            order.OrderId, order.CustomerId
        });
    }

    public async Task CancelOrder(long id)
    {
        var order = _supplierService.CancelOrder(id);
        _unitOfWork.Commit();

        await _publisher.Publish<SupplierCancelledEvent>(new
        {
            order.OrderId
        });
    }
}