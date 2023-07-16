using MassTransit;
using OrderService.Domain.Events.SupplierService;
using OrderService.Domain.Services.Interfaces;

namespace OrderService.Application.Consumers;

public class SupplierCancelledConsumer : IConsumer<SupplierCancelledEvent>
{
    private readonly IOrderService _service;

    public SupplierCancelledConsumer(IOrderService service)
    {
        _service = service;
    }

    public async Task Consume(ConsumeContext<SupplierCancelledEvent> context)
    {
        await _service.HandleOrderCancelledEvent(context.Message.OrderId);
    }
}