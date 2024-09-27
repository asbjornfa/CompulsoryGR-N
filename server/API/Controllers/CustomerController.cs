using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomer _customerService;

    public CustomerController(ICustomer customerService)
    {
        _customerService = customerService;
    }



    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetCustomers()
    {
        var allCustomers = await _customerService.GetAllCustomers();
        return Ok(allCustomers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerById(id);
        
        return Ok(customer);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateCustomer([FromBody] RequestCreateCustomerDTO request)
    {
        var response = await _customerService.CreateCustomer(request);
        return CreatedAtAction(nameof(GetCustomerById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] RequestCreateCustomerDTO request)
    {
        var response = await _customerService.UpdateCustomer(id, request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        var response = await _customerService.GetCustomerById(id);
        if (response == null)
        {
            return NotFound();
        }
        
        await _customerService.DeleteCustomer(id);
        return Ok();
    }
    
    
}