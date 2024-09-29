using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.Interfaces;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderEntryController : ControllerBase
{
    private readonly IOrderEntry _orderEntryService;

    public OrderEntryController(IOrderEntry orderEntryService)
    {
        _orderEntryService = orderEntryService;
    }
    [HttpGet]
    public async Task<ActionResult> GetOrderEntries()
    {
        var allOrderEntries = await _orderEntryService.GetAllOrderEntries();
        return Ok(allOrderEntries);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderEntry>> GetOrderEntryById(int id)
    {
        var orderEntry = await _orderEntryService.GetOrderEntryById(id);
        return Ok(orderEntry);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrderEntry([FromBody] RequestCreateOrderEntryDTO request)
    {
        var response = await _orderEntryService.CreateOrderEntry(request);
        return CreatedAtAction(nameof(GetOrderEntries), new { id = response.Id }, response);
    }
}
