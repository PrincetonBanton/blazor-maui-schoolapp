using Microsoft.AspNetCore.Mvc;
using SchoolApp.Shared.Models;
using SchoolApp.Server.Data;

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

    // GET: api/Subject/{id}
    [HttpGet("{id}")]
    public ActionResult<Subject> GetSubject(Guid id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject == null) return NotFound();
        return Ok(subject);
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
}
