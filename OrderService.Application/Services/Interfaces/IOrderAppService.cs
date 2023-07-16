using OrderService.Application.Dtos.Requests;
using OrderService.Application.Dtos.Responses;

namespace OrderService.Application.Services.Interfaces;

public interface IOrderAppService
{
    OrderResponse GetById(long id);
    Task<OrderResponse> Add(OrderRequest request);
}