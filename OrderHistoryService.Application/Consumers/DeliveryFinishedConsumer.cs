using MassTransit;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Application.Consumers;

public class DeliveryFinishedConsumer : IConsumer<DeliveryFinishedEvent>
{
    private readonly IOrderHistoryService _service;

    public DeliveryFinishedConsumer(IOrderHistoryService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<DeliveryFinishedEvent> context)
    {
        _service.HandleOrderCompletedEvent(context.Message.OrderId);
        return Task.CompletedTask;
    }
}