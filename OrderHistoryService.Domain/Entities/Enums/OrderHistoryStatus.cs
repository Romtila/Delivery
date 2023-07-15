using System.ComponentModel;

namespace OrderHistoryService.Domain.Entities.Enums;

public enum OrderHistoryStatus
{
    Preparing,
    AwaitingPickup,
    Delivering,
    Completed,
    Cancelled
}