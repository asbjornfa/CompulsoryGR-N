using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;
using Service.DTO.Response;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaperController(MyDbContext context) : ControllerBase{


    [HttpGet]
    [Route("")]
    public ActionResult GetPapers()
    {
        return Ok(context.Papers.Where(p => p.Discontinued == false).ToList());
    }

    [HttpPost]
    [Route("")]
    public ActionResult CreatePaper([FromBody] RequestCreatePaperDTO request)
    {
        // Convert the request DTO to a Paper entity
        var paper = new Paper
        {
            Name = request.Name,
            Discontinued = request.Discontinued,
            Stock = request.Stock,
            Price = request.Price
        };
    
        // Add the new Paper entity to the database
        context.Papers.Add(paper);
        context.SaveChanges();

        // Create a response DTO from the newly created Paper entity
        var response = new ResponseCreatePaperDTO
        {
            Id = paper.Id,
            Name = paper.Name,
            Discontinued = paper.Discontinued,
            Stock = paper.Stock,
            Price = paper.Price
        };

        // Return the response DTO
        return Ok(response);
    }

    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeletePaper(int id)
    {
        var paper = context.Papers.Find(id);
        if (paper == null)
        {
            return NotFound();
        }
        context.Papers.Remove(paper);
        context.SaveChanges();
        return Ok();
    }
    
    
}