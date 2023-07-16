using DeliveryService.Domain.Events.SupplierService;
using DeliveryService.Domain.Services.Interfaces;
using MassTransit;

namespace DeliveryService.Application.Consumers;

public class SupplierFinishedConsumer : IConsumer<SupplierFinishedEvent>
{
    private readonly IDeliveryService _service;

    public SupplierFinishedConsumer(IDeliveryService service)
    {
        _service = service;
    }

    public async Task Consume(ConsumeContext<SupplierFinishedEvent> context)
    {
        await _service.HandleDelivery(context.Message);
    }
}