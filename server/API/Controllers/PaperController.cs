using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/paper")]
public class PaperController(MyDbContext context) : ControllerBase{


    [HttpGet]
    [Route("")]
    public ActionResult GetPapers()
    {
        return Ok(context.Papers.ToList());
    }

    [HttpPost]
    [Route("{id}")]
    public ActionResult CreatePaper([FromBody] Paper paper)
    {
        context.Add(paper);
        context.SaveChanges();
        return Ok(paper);
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