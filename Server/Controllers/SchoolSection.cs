using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolSectionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SchoolSectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SchoolSection
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SchoolSection>>> GetSchoolSections()
    {
        var sections = await _context.SchoolSections
            .Include(s => s.GradeLevel)           // include grade level info
                .ThenInclude(g => g.SchoolYear)   // also include school year info
            .OrderBy(s => s.SectionName)
            .ToListAsync();

        return Ok(sections);
    }

    // GET: api/SchoolSection/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolSection>> GetSchoolSection(Guid id)
    {
        var section = await _context.SchoolSections
            .Include(s => s.GradeLevel)
                .ThenInclude(g => g.SchoolYear)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (section == null) return NotFound();

        return Ok(section);
    }

    // POST: api/SchoolSection
    [HttpPost]
    public async Task<ActionResult<SchoolSection>> CreateSchoolSection(SchoolSection schoolSection)
    {
        schoolSection.Id = Guid.NewGuid();

        _context.SchoolSections.Add(schoolSection);
        await _context.SaveChangesAsync();

        return Ok(schoolSection);
    }

    // PUT: api/SchoolSection/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSchoolSection(Guid id, SchoolSection updated)
    {
        var existing = await _context.SchoolSections.FirstOrDefaultAsync(s => s.Id == id);
        if (existing == null) return NotFound();

        existing.SectionName = updated.SectionName;
        existing.GradeLevelId = updated.GradeLevelId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/SchoolSection/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchoolSection(Guid id)
    {
        var existing = await _context.SchoolSections.FirstOrDefaultAsync(s => s.Id == id);
        if (existing == null) return NotFound();

        _context.SchoolSections.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
