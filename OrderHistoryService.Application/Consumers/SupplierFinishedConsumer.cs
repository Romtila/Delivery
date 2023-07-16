using MassTransit;
using OrderHistoryService.Domain.Events.SupplierService;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Application.Consumers;

public class SupplierFinishedConsumer : IConsumer<SupplierFinishedEvent>
{
    private readonly IOrderHistoryService _service;

    public SupplierFinishedConsumer(IOrderHistoryService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<SupplierFinishedEvent> context)
    {
        _service.HandleSupplierFinishedEvent(context.Message);
        return Task.CompletedTask;
    }
}