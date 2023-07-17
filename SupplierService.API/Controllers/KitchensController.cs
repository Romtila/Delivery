using Microsoft.AspNetCore.Mvc;
using SupplierService.Application.Dtos.Responses;
using SupplierService.Application.Services.Interfaces;

namespace SupplierService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KitchensController : ControllerBase
{
    private readonly ISupplierAppService _kitchenAppService;

    public KitchensController(ISupplierAppService kitchenAppService)
    {
        _kitchenAppService = kitchenAppService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SupplierOrderResponse>> Get()
    {
        return Ok(_kitchenAppService.Get());
    }

    [HttpGet("{id:int}")]
    public ActionResult<IEnumerable<SupplierOrderResponse>> Get(int id)
    {
        return Ok(_kitchenAppService.Get(id));
    }

    [HttpPut("finish/{id:int}")]
    public async Task<ActionResult<SupplierOrderResponse>> FinishOrder([FromRoute] int id)
    {
        await _kitchenAppService.FinishOrder(id);
        return Ok();
    }

    [HttpDelete("cancel/{id:int}")]
    public async Task<ActionResult<SupplierOrderResponse>> CancelOrder([FromRoute] int id)
    {
        await _kitchenAppService.CancelOrder(id);
        return Ok();
    }
}