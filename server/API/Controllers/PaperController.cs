using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.DTO.Response;
using Service.Interfaces;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PaperController : ControllerBase
{
    private readonly IPaper _paperService;

    public PaperController(IPaper paperService)
    {
        _paperService = paperService;
    }


    [HttpGet]
    [Route("")]
    public async Task<ActionResult> GetPapers(
    [FromQuery] string? search = null,
    [FromQuery] string? sortBy = null,
    [FromQuery] string? sortOrder = "asc",
    [FromQuery] double? minPrice = null,
    [FromQuery] double? maxPrice = null)
    {
        //Get all the papers
        var allPapers = await _paperService.GetAllPapers();
        
        //Filter papers by price 
        if(minPrice.HasValue)
        {
        allPapers = allPapers.Where(p => p.Price >= minPrice.Value).ToList();
        }
        if(maxPrice.HasValue)
        {
        allPapers = allPapers.Where(p => p.Price <= maxPrice.Value).ToList();
        }
        
        //Full text search on name 
        if(!string.IsNullOrEmpty(search))
        {
        allPapers = allPapers.Where(p => 
        p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        
        //Sorting by criteria
        if(!string.IsNullOrEmpty(sortBy))
        {
        switch(sortBy.ToLower())
        {
        case "price":
        allPapers = sortOrder == "desc"
        ? allPapers.OrderByDescending(p => p.Price).ToList()
        : allPapers.OrderBy(p => p.Price).ToList();
        break;
        
        case "name":
        allPapers = sortOrder == "desc"
        ? allPapers.OrderByDescending(p => p.Name).ToList()
        : allPapers.OrderBy(p => p.Name).ToList();
        break;
        
        case "stock":
        allPapers = sortOrder == "desc"
        ? allPapers.OrderByDescending(p => p.Stock).ToList()
        : allPapers.OrderBy(p => p.Stock).ToList();
        break;
        
        default:
        break;
        }
        }
        //return filtered, sorted papers
        return Ok(allPapers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Paper>> GetPaperById(int id)
    {
        var paper = await _paperService.GetPaperById(id);

        return Ok(paper);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreatePaper([FromBody] RequestCreatePaperDTO request)
    { 
        
        var response = await _paperService.CreatePaper(request);
        return CreatedAtAction(nameof(GetPaperById), new { id = response.Id }, response);
       // return Ok(response);
}


    [HttpPut("{id}")]
    public async Task<ActionResult >UpdatePaper(int id, [FromBody] RequestCreatePaperDTO request)
    
    {
        var updatePaper = await _paperService.UpdatePaper(id, request);
        return Ok(updatePaper);
    }


[HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeletePaper(int id)
    {
        var paper = await _paperService.GetPaperById(id);
        if (paper == null)
        {
            return NotFound();
        }

        await _paperService.DeletePaper(id);
        return Ok();
    }
    
    
}