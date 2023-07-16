using MassTransit;
using SupplierService.Application.MapperProfiles;
using SupplierService.Domain.Events.OrderService;
using SupplierService.Domain.Services.Interfaces;

namespace SupplierService.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ISupplierService _service;

    public OrderCreatedConsumer(ISupplierService service)
    {
        _service = service;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var entity = context.Message.OrderCreatedEventToSupplierOrder();
        _service.HandleNewOrder(entity);

        return Task.CompletedTask;
    }
}