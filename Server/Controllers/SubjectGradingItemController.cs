using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectGradingItemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubjectGradingItemController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SubjectGradingItem
    [HttpGet]
    public ActionResult<IEnumerable<SubjectGradingItem>> GetAll()
    {
        return Ok(_context.SubjectGradingItems.ToList());
    }

    // GET: api/SubjectGradingItem/by-subject/{subjectId}
    [HttpGet("by-subject/{subjectId}")]
    public ActionResult<IEnumerable<SubjectGradingItem>> GetBySubject(Guid subjectId)
    {
        var items = _context.SubjectGradingItems
            .Where(x => x.SubjectId == subjectId)
            .ToList();

        return Ok(items);
    }

    // POST: api/SubjectGradingItem (Upsert)
    [HttpPost]
    public ActionResult<SubjectGradingItem> Upsert(SubjectGradingItem input)
    {
        if (input == null)
            return BadRequest();

        var existing = _context.SubjectGradingItems.FirstOrDefault(x =>
            x.SubjectId == input.SubjectId &&
            x.SubcomponentId == input.SubcomponentId &&
            x.GradingPeriod == input.GradingPeriod
        );

        if (existing != null)
        {
            // Update existing
            existing.ItemCount = input.ItemCount;
            existing.CreatedAt = DateTime.Now;
        }
        else
        {
            // Create new
            input.Id = Guid.NewGuid();
            input.CreatedAt = DateTime.Now;
            _context.SubjectGradingItems.Add(input);
        }

        _context.SaveChanges();
        return Ok(input);
    }

    // DELETE: api/SubjectGradingItem/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _context.SubjectGradingItems.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();

        _context.SubjectGradingItems.Remove(item);
        _context.SaveChanges();

        return NoContent();
    }
}
