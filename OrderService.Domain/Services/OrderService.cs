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

    public async Task<Order> Validate(long id, CancellationToken ct)
    {
        var order = await _repository.FindAsync(id, ct);

        if (order is null)
            throw new OrderNotFoundException();

        return order;
    }

    public async Task<Order> Add(CreateNewOrderCommand command, CancellationToken ct)
    {
        var orderTotal = command.Items.Sum(x => x.Price * x.Quantity);

        var orderItems = command.Items.Select(CreateOrderItemFromCommand).ToList();
        var order = CreateOrderFromCommand(command, orderItems, orderTotal);
        await _repository.AddAsync(order, ct);
        await _orderItemRepository.AddAsync(orderItems, ct);

        await ChargeCustomer(command.CustomerId, orderTotal, ct);

        return order;
    }

    public async Task HandleOrderCancelledEvent(long orderId, CancellationToken ct)
    {
        var order = await Validate(orderId, ct);
        await RefundCustomer(order.CustomerId, order.Total, ct);
    }

    private async Task RefundCustomer(long customerId, decimal orderTotal, CancellationToken ct)
    {
        using var response = await _httpClient.PutAsJsonAsync("balances", RefundCustomerRequest.Create(customerId, orderTotal), cancellationToken: ct);

        if (!response.IsSuccessStatusCode)
            throw new ErrorRefundingCustomer();
    }

    private async Task ChargeCustomer(long customerId, decimal orderTotal, CancellationToken ct)
    {
        using var response = await _httpClient.PutAsJsonAsync("charges", ChargeCustomerRequest.Create(customerId, orderTotal), cancellationToken: ct);

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