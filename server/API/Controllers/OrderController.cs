using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrder _orderSevrice;

    public OrderController(IOrder orderSevrice)
    {
        _orderSevrice = orderSevrice;
    }
    
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<ResponseCreateOrderDTO>>> GetOrders()
    
    {
        var allOrders = await _orderSevrice.GetAllOrders();
        return Ok(allOrders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var order = await _orderSevrice.GetOrderById(id);
        
        return Ok(order);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateOrder([FromBody] RequestCreateOrderDTO request)
    {
        var response = await _orderSevrice.CreateOrder(request);
        return CreatedAtAction(nameof(GetOrderById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, [FromBody] RequestCreateOrderDTO request)
    {
        var response = await _orderSevrice.UpdateOrder(id, request);
        return Ok(response);
    }
    

    

}