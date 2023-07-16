using System.Net.Http.Json;
using OrderService.Domain.Commands;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Domain.Repositories;
using OrderService.Domain.Requests;
using OrderService.Domain.Services.Interfaces;

namespace OrderService.Domain.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository, IOrderItemRepository orderItemRepository, HttpClient httpClient)
    {
        _repository = repository;
        _orderItemRepository = orderItemRepository;
        _httpClient = httpClient;
    }

    public Order Validate(long id)
    {
        var order = _repository.Find(id);

        if (order is null) 
            throw new OrderNotFoundException();

        return order;
    }

    public async Task<Order> Add(CreateNewOrderCommand command)
    {
        var orderTotal = command.Items.Sum(x => x.Price * x.Quantity);

        var orderItems = command.Items.Select(CreateOrderItemFromCommand).ToList();
        var order = CreateOrderFromCommand(command, orderItems, orderTotal);
        
        _repository.Add(order);
        _orderItemRepository.Add(orderItems);

        await ChargeCustomer(command.CustomerId, orderTotal);

        return order;
    }

    public async Task HandleOrderCancelledEvent(long orderId)
    {
        var order = Validate(orderId);
        await RefundCustomer(order.CustomerId, order.Total);
    }

    private async Task RefundCustomer(long customerId, decimal orderTotal)
    {
        using var response = await _httpClient.PutAsJsonAsync("balances", RefundCustomerRequest.Create(customerId, orderTotal));

        if (!response.IsSuccessStatusCode) 
            throw new ErrorRefundingCustomer();
    }

    private async Task ChargeCustomer(long customerId, decimal orderTotal)
    {
        using var response = await _httpClient.PutAsJsonAsync("charges", ChargeCustomerRequest.Create(customerId, orderTotal));

        if (!response.IsSuccessStatusCode) 
            throw new ErrorChargingCustomer();
    }

    private static Order CreateOrderFromCommand(CreateNewOrderCommand command, IEnumerable<OrderItem> orderItems, decimal orderTotal)
    {
        return new Order {CustomerId = command.CustomerId, Items = orderItems, Total = orderTotal};
    }

    private static OrderItem CreateOrderItemFromCommand(CreateNewOrderItemCommand command)
    {
        return new OrderItem {Name = command.Name, Price = command.Price, Quantity = command.Quantity};
    }
}