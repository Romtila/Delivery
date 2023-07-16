using MassTransit;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Application.Consumers;

public class DeliveryStartedConsumer : IConsumer<DeliveryStartedEvent>
{
    private readonly IOrderHistoryService _service;

    public DeliveryStartedConsumer(IOrderHistoryService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<DeliveryStartedEvent> context)
    {
        _service.HandleDeliveryStartedEvent(context.Message);
        return Task.CompletedTask;
    }
}