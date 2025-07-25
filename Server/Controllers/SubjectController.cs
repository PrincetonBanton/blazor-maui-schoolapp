using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Subject
    [HttpGet]
    public ActionResult<IEnumerable<Subject>> GetSubjects()
    {
        return Ok(_context.Subjects.ToList());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Subject>> GetSubject(Guid id)
    {
        var subject = await _context.Subjects
            .Include(s => s.Components)
                .ThenInclude(c => c.Subcomponents)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (subject == null)
        {
            return NotFound();
        }

        return subject;
    }


    // POST: api/Subject
    [HttpPost]
    public ActionResult<Subject> CreateSubject(Subject subject)
    {
        subject.Id = Guid.NewGuid();
        _context.Subjects.Add(subject);
        _context.SaveChanges();
        return Ok(subject);
    }

    // PUT: api/Subject/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateSubject(Guid id, Subject updatedSubject)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null) return NotFound();

        subject.Name = updatedSubject.Name;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Subject/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteSubject(Guid id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null) return NotFound();

        _context.Subjects.Remove(subject);
        _context.SaveChanges();

        return NoContent();
    }

    // GET: api/subject/{subjectId}/components
    [HttpGet("{subjectId}/components")]
    public ActionResult<IEnumerable<Component>> GetComponentsBySubject(Guid subjectId)
    {
        var components = _context.Components
            .Where(c => c.SubjectId == subjectId)
            .ToList();

        return Ok(components);
    }

    // PUT: api/subject/validate/{id}
    [HttpPut("validate/{id}")]
    public IActionResult ValidateTemplate(Guid id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null)
            return NotFound();

        subject.IsValidated = true;
        _context.SaveChanges();

        return NoContent();
    }


}
