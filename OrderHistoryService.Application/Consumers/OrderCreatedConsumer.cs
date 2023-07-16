using MassTransit;
using OrderHistoryService.Application.MapperProfiles;
using OrderHistoryService.Domain.Events.OrderService;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IOrderHistoryService _service;

    public OrderCreatedConsumer(IOrderHistoryService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var entity = context.Message.OrderCreatedEventToOrderHistory();
        _service.HandleNewOrder(entity);

        return Task.CompletedTask;
    }
}