using Microsoft.AspNetCore.Mvc;
using OrderHistoryService.Application.Dtos.Responses;
using OrderHistoryService.Application.Services.Interfaces;

namespace OrderHistoryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersHistoryController : ControllerBase
{
    private readonly IOrderHistoryAppService _orderHistoryAppService;

    public OrdersHistoryController(IOrderHistoryAppService orderHistoryAppService)
    {
        _orderHistoryAppService = orderHistoryAppService;
    }

    [HttpGet("{id:int}")]
    public ActionResult<OrderHistoryResponse> Get(int id)
    {
        return Ok(_orderHistoryAppService.GetById(id));
    }

    [HttpGet("orders/{orderId:int}")]
    public ActionResult<OrderHistoryResponse> GetByOrderId(int orderId)
    {
        return Ok(_orderHistoryAppService.GetByOrderId(orderId));
    }

    [HttpGet("customers/{customerId:int}")]
    public ActionResult<IEnumerable<OrderHistoryResponse>> GetByCustomerId(int customerId)
    {
        return Ok(_orderHistoryAppService.GetByCustomerId(customerId));
    }
}