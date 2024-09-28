using DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.Interfaces;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IProperties _propertiesService;

    public PropertiesController(IProperties propertiesService)
    {
        _propertiesService = propertiesService;
    }


    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetProperties()
    {
        var allProperties = await _propertiesService.GetAllProperties();
        return Ok(allProperties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Properties>> GetPropertiesById(int id)
    {
        var properties = await _propertiesService.GetPropertyById(id);
        
        return Ok(properties);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateProperty([FromBody] RequestCreatePropertyDTO request)
    {

        var response = await _propertiesService.CreateProperty(request);
        return CreatedAtAction(nameof(GetPropertiesById), new { id = response.Id }, response);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateProperty(int id, [FromBody] RequestCreatePropertyDTO request)
    {
        var updatedProperties = await _propertiesService.UpdateProperty(id, request);
        return Ok(updatedProperties);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteProperty(int id)
    {
        var properties = await _propertiesService.GetPropertyById(id);
        if (properties == null)
        {
            return NotFound();
        }
        
        await _propertiesService.DeleteProperty(id);
        return Ok();
    }
}