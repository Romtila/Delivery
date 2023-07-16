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

    public DeliveryOrder Validate(long id)
    {
        var order = _repository.Find(id);

        if (order is null)
            throw new DeliveryOrderNotFoundException();

        return order;
    }

    public DeliveryOrder StartDelivery(long id)
    {
        var order = Validate(id);
        ValidateUpdatingOrder(order, DeliveryOrderStatus.Pending);

        order.Status = DeliveryOrderStatus.Delivering;
        _repository.Update(order);

        return order;
    }

    public DeliveryOrder FinishDelivery(long id)
    {
        var order = Validate(id);
        ValidateUpdatingOrder(order, DeliveryOrderStatus.Delivering);

        order.Status = DeliveryOrderStatus.Delivered;
        _repository.Update(order);

        return order;
    }

    public async Task HandleDelivery(SupplierFinishedEvent msg)
    {
        var customer = await _httpClient.GetFromJsonAsync<CustomerResponse>(msg.CustomerId.ToString());

        if (customer is null)
            throw new BusinessLogicException("Error retrieving customer data");

        var entity = CreateDeliveryOrderFromMessage(msg, customer);

        _repository.Add(entity);
        _repository.Commit();
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

    private static void ValidateUpdatingOrder(DeliveryOrder order, DeliveryOrderStatus allowedStatus)
    {
        if (order.Status == allowedStatus)
            return;

        throw new DeliveryOrderUpdateException(order.Status.GetDescription(true));
    }
}