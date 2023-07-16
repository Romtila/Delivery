using Delivery.BaseLib.Infrastructure.Transactions;
using MassTransit;
using OrderService.Application.Dtos.Requests;
using OrderService.Application.Dtos.Responses;
using OrderService.Application.MapperProfiles;
using OrderService.Application.Services.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services.Interfaces;

namespace OrderService.Application.Services;

public class OrderAppService : IOrderAppService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IPublishEndpoint _publisher;
    private readonly IUnitOfWork _unitOfWork;

    public OrderAppService(IOrderService orderService, IOrderRepository orderRepository, IUnitOfWork unitOfWork, IPublishEndpoint publisher)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public OrderResponse GetById(long id)
    {
        var order = _orderService.Validate(id);

        return order.OrderToOrderResponse();
    }

    public async Task<OrderResponse> Add(OrderRequest request)
    {
        var command = request.OrderRequestToCreateNewOrderCommand();

        var order = await _orderService.Add(command);
        _unitOfWork.Commit();

        await PublishNewOrder(order);

        return order.OrderToOrderResponse();
    }

    private async Task PublishNewOrder(Order order)
    {
        await _publisher.Publish<OrderCreatedEvent>(new
        {
            OrderId = order.Id, order.CustomerId, order.Items, order.Total
        });
    }
}