using System.Net.Http.Json;
using Delivery.BaseLib.Common.Enums;
using Delivery.BaseLib.Domain.Exceptions;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Entities.Enums;
using DeliveryService.Domain.Events.SupplierService;
using DeliveryService.Domain.Exceptions;
using DeliveryService.Domain.Repositories;
using DeliveryService.Domain.Responses;
using DeliveryService.Domain.Services.Interfaces;

namespace DeliveryService.Domain.Services;

public class DeliveryService : IDeliveryService
{
    private readonly HttpClient _httpClient;
    private readonly ISupplierRepository _repository;

    public DeliveryService(ISupplierRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<DeliveryOrder> Validate(long id, CancellationToken ct)
    {
        var order = await _repository.FindAsync(id, ct);

        if (order is null)
            throw new DeliveryOrderNotFoundException();

        return order;
    }

    public async Task<DeliveryOrder> StartDelivery(long id, CancellationToken ct)
    {
        var order = await Validate(id, ct);
        ValidateUpdatingOrder(order, DeliveryOrderStatus.Pending);

        order.Status = DeliveryOrderStatus.Delivering;
        await _repository.UpdateAsync(order, ct);

        return order;
    }

    public async Task<DeliveryOrder> FinishDelivery(long id, CancellationToken ct)
    {
        var order = await Validate(id, ct);
        ValidateUpdatingOrder(order, DeliveryOrderStatus.Delivering);

        order.Status = DeliveryOrderStatus.Delivered;
        await _repository.UpdateAsync(order, ct);

        return order;
    }

    public async Task HandleDelivery(SupplierFinishedEvent msg, CancellationToken ct)
    {
        var customer = await _httpClient.GetFromJsonAsync<CustomerResponse>(msg.CustomerId.ToString(), cancellationToken: ct);

        if (customer is null)
            throw new BusinessLogicException("Error retrieving customer data");

        var entity = CreateDeliveryOrderFromMessage(msg, customer);

        await _repository.AddAsync(entity, ct);
    }

    private static void ValidateUpdatingOrder(DeliveryOrder order, DeliveryOrderStatus allowedStatus)
    {
        if (order.Status == allowedStatus)
            return;

        throw new DeliveryOrderUpdateException(order.Status.GetDescription(true));
    }

    private static DeliveryOrder CreateDeliveryOrderFromMessage(SupplierFinishedEvent msg, CustomerResponse customer)
    {
        return new DeliveryOrder
        {
            OrderId = msg.OrderId
            , CustomerId = msg.CustomerId
            , CustomerName = customer.Name
            , DeliveryAddress = customer.Address
            , Status = DeliveryOrderStatus.Pending
        };
    }
}