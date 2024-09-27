using DataAccess;
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
    public async Task<ActionResult> GetPapers()
    {
        var allPapers = await _paperService.GetAllPapers();
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