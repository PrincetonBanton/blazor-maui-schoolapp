using Microsoft.AspNetCore.Mvc;
using SchoolApp.Shared.Models;
using SchoolApp.Server.Data;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ComponentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Component
    [HttpGet]
    public ActionResult<IEnumerable<Component>> GetComponents()
    {
        return Ok(_context.Components.ToList());
    }

    // GET: api/Component/by-subject/{subjectId}
    [HttpGet("by-subject/{subjectId}")]
    public ActionResult<IEnumerable<Component>> GetBySubject(Guid subjectId)
    {
        var components = _context.Components
            .Where(c => c.SubjectId == subjectId)
            .ToList();
        return Ok(components);
    }

    // GET: api/Component/{id}
    [HttpGet("{id}")]
    public ActionResult<Component> GetComponent(Guid id)
    {
        var component = _context.Components.FirstOrDefault(c => c.Id == id);
        if (component == null) return NotFound();
        return Ok(component);
    }

    // POST: api/Component
    [HttpPost]
    public ActionResult<Component> CreateComponent(Component component)
    {
        component.Id = Guid.NewGuid();
        _context.Components.Add(component);
        _context.SaveChanges();
        return Ok(component);
    }

    // PUT: api/Component/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateComponent(Guid id, Component updated)
    {
        var component = _context.Components.FirstOrDefault(c => c.Id == id);
        if (component == null) return NotFound();

        component.Name = updated.Name;
        component.Percentage = updated.Percentage;
        component.SubjectId = updated.SubjectId;

        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/Component/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteComponent(Guid id)
    {
        var component = _context.Components.FirstOrDefault(c => c.Id == id);
        if (component == null) return NotFound();

        _context.Components.Remove(component);
        _context.SaveChanges();
        return NoContent();
    }
}
