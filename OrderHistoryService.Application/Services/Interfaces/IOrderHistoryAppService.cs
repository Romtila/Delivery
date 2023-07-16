using OrderHistoryService.Application.Dtos.Responses;

namespace OrderHistoryService.Application.Services.Interfaces;

public interface IOrderHistoryAppService
{
    OrderHistoryResponse GetById(long id);
    OrderHistoryResponse GetByOrderId(long orderId);
    IEnumerable<OrderHistoryResponse> GetByCustomerId(long customerId);
}