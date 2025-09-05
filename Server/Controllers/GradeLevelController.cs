using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;
using System.Diagnostics;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeLevelController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GradeLevelController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/GradeLevel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GradeLevel>>> GetGradeLevels()
    {
        var grades = await _context.GradeLevels
            .Include(g => g.SchoolYear) // include school year info
            .OrderBy(g => g.LevelName)
            .ToListAsync();

        return Ok(grades);
    }

    // GET: api/GradeLevel/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<GradeLevel>> GetGradeLevel(Guid id)
    {
        var grade = await _context.GradeLevels
            .Include(g => g.SchoolYear)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (grade == null) return NotFound();

        return Ok(grade);
    }

    // POST: api/GradeLevel
    [HttpPost]
    public async Task<ActionResult<GradeLevel>> CreateGradeLevel(GradeLevel gradeLevel)
    {
        gradeLevel.Id = Guid.NewGuid();

        _context.GradeLevels.Add(gradeLevel);
        await _context.SaveChangesAsync();

        return Ok(gradeLevel);
    }

    // PUT: api/GradeLevel/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGradeLevel(Guid id, GradeLevel updated)
    {
        var existing = await _context.GradeLevels.FirstOrDefaultAsync(g => g.Id == id);
        if (existing == null) return NotFound();

        existing.LevelName = updated.LevelName;
        existing.SchoolYearId = updated.SchoolYearId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/GradeLevel/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGradeLevel(Guid id)
    {
        var existing = await _context.GradeLevels.FirstOrDefaultAsync(g => g.Id == id);
        if (existing == null) return NotFound();

        _context.GradeLevels.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
