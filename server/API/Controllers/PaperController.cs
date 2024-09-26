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

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreatePaper([FromBody] RequestCreatePaperDTO request)

        {
            var createdPaper = await _paperService.CreatePaper(request);
            return CreatedAtAction(nameof(createdPaper), new { id = createdPaper.Id}, createdPaper);
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