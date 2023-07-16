using Delivery.BaseLib.Infrastructure.Transactions;
using DeliveryService.Application.Dtos.Responses;
using DeliveryService.Application.MapperProfiles;
using DeliveryService.Application.Services.Interfaces;
using DeliveryService.Domain.Events;
using DeliveryService.Domain.Repositories;
using DeliveryService.Domain.Services.Interfaces;
using MassTransit;

namespace DeliveryService.Application.Services;

public class DeliveryAppService : IDeliveryAppService
{
    private readonly IDeliveryService _deliveryService;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryAppService(IDeliveryService deliveryService, ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork, IPublishEndpoint publisher)
    {
        _deliveryService = deliveryService;
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public DeliveryOrderResponse Get(long id)
    {
        var supplierOrder = _deliveryService.Validate(id);

        return supplierOrder.DeliveryOrderToDeliveryOrderResponse();
    }

    public IEnumerable<DeliveryOrderResponse> Get()
    {
        var orders = _supplierRepository.Query().ToList();
        return orders.DeliveryOrdersToDeliveryOrderResponses();
    }

    public async Task StartDelivery(long id)
    {
        var order = _deliveryService.StartDelivery(id);
        _unitOfWork.Commit();

        await _publisher.Publish<DeliveryStartedEvent>(new
        {
            order.OrderId, order.DeliveryAddress
        });
    }

    public async Task FinishDelivery(long id)
    {
        var order = _deliveryService.FinishDelivery(id);
        _unitOfWork.Commit();

        await _publisher.Publish<DeliveryFinishedEvent>(new
        {
            order.OrderId
        });
    }
}