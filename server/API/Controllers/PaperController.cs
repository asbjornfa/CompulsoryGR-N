using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Request;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaperController(MyDbContext context) : ControllerBase{


    [HttpGet]
    [Route("")]
    public ActionResult GetPapers()
    {
        return Ok(context.Papers.ToList());
    }

    [HttpPost]
    [Route("")]
    public ActionResult CreatePaper([FromBody] RequestCreatePaperDTO request)
    {
        context.Add(request);
        context.SaveChanges();
        return Ok(request);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult DeletePaper([FromBody] Paper paper)
    {
        context.Remove(paper);
        context.SaveChanges();
        return Ok();
    }
    
    
}