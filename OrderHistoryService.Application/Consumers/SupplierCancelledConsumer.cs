using MassTransit;
using OrderHistoryService.Domain.Events.SupplierService;
using OrderHistoryService.Domain.Services.Interfaces;

namespace OrderHistoryService.Application.Consumers;

public class SupplierCancelledConsumer : IConsumer<SupplierCancelledEvent>
{
    private readonly IOrderHistoryService _service;

    public SupplierCancelledConsumer(IOrderHistoryService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<SupplierCancelledEvent> context)
    {
        _service.HandleOrderCancelledEvent(context.Message.OrderId);
        return Task.CompletedTask;
    }
}