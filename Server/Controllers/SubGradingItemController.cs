using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubGradingItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubGradingItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SubGradingItem
    [HttpGet]
    public ActionResult<IEnumerable<SubGradingItem>> GetAll()
    {
        return Ok(_context.SubGradingItems.ToList());
    }

    // GET: api/SubGradingItem/by-subject/{subjectId}
    [HttpGet("by-subject/{subjectId}")]
    public ActionResult<IEnumerable<SubGradingItem>> GetBySubject(Guid subjectId)
    {
        var items = _context.SubGradingItems
            .Where(x => x.SubjectId == subjectId)
            .ToList();

        return Ok(items);
    }

    // POST: api/SubGradingItem (For creating new items)
    [HttpPost]
    public ActionResult<SubGradingItem> Create(SubGradingItem newItem)
    {
        if (newItem == null)
            return BadRequest();
        newItem.Id = Guid.NewGuid();
        newItem.CreatedAt = DateTime.Now;

        _context.SubGradingItems.Add(newItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    // GET: api/SubGradingItem/{id}
    [HttpGet("{id}")]
    public ActionResult<SubGradingItem> GetById(Guid id)
    {
        var item = _context.SubGradingItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    // PUT: api/SubGradingItem/{id}
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, SubGradingItem updatedItem)
    {
        if (updatedItem == null || id != updatedItem.Id)
        {
            return BadRequest("Mismatched ID or invalid data.");
        }

        var existingItem = _context.SubGradingItems.FirstOrDefault(x => x.Id == id);
        if (existingItem == null)
        {
            return NotFound($"Item with ID {id} not found.");
        }

        // Update properties
        existingItem.ItemCount = updatedItem.ItemCount;
        existingItem.GradingPeriod = updatedItem.GradingPeriod;
        existingItem.SubcomponentId = updatedItem.SubcomponentId;
        existingItem.SubjectId = updatedItem.SubjectId;
        existingItem.CreatedAt = DateTime.Now;

        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/SubGradingItem/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _context.SubGradingItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();

        _context.SubGradingItems.Remove(item);
        _context.SaveChanges();

        return NoContent();
    }
}
