using Microsoft.AspNetCore.Mvc;
using SchoolApp.Shared.Models;
using SchoolApp.Server.Data;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubcomponentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public SubcomponentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Subcomponent
    [HttpGet]
    public ActionResult<IEnumerable<Subcomponent>> GetAll()
    {
        return Ok(_context.Subcomponents.ToList());
    }

    // GET: api/Subcomponent/by-component/{componentId}
    [HttpGet("by-component/{componentId}")]
    public ActionResult<IEnumerable<Subcomponent>> GetByComponent(Guid componentId)
    {
        var subs = _context.Subcomponents
            .Where(s => s.ComponentId == componentId)
            .ToList();
        return Ok(subs);
    }

    // GET: api/Subcomponent/{id}
    [HttpGet("{id}")]
    public ActionResult<Subcomponent> GetById(Guid id)
    {
        var sub = _context.Subcomponents.FirstOrDefault(s => s.Id == id);
        if (sub == null) return NotFound();
        return Ok(sub);
    }

    // POST: api/Subcomponent
    [HttpPost]
    public ActionResult<Subcomponent> Create(Subcomponent sub)
    {
        sub.Id = Guid.NewGuid();
        _context.Subcomponents.Add(sub);
        _context.SaveChanges();
        return Ok(sub);
    }

    // PUT: api/Subcomponent/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Subcomponent updated)
    {
        var sub = _context.Subcomponents.FirstOrDefault(s => s.Id == id);
        if (sub == null) return NotFound();

        sub.Name = updated.Name;
        sub.Percentage = updated.Percentage;
        //sub.Items = updated.Items;
        //sub.Score = updated.Score;
        sub.ComponentId = updated.ComponentId;

        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/Subcomponent/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var sub = _context.Subcomponents.FirstOrDefault(s => s.Id == id);
        if (sub == null) return NotFound();

        _context.Subcomponents.Remove(sub);
        _context.SaveChanges();
        return NoContent();
    }
}
