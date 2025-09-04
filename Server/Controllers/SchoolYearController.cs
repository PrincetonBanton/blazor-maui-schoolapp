using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolYearController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SchoolYearController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SchoolYear
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SchoolYear>>> GetSchoolYears()
    {
        var years = await _context.SchoolYears
            .OrderByDescending(y => y.StartDate)
            .ToListAsync();

        return Ok(years);
    }

    // GET: api/SchoolYear/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolYear>> GetSchoolYear(Guid id)
    {
        var year = await _context.SchoolYears.FirstOrDefaultAsync(y => y.Id == id);
        if (year == null)
            return NotFound();

        return Ok(year);
    }

    // POST: api/SchoolYear
    [HttpPost]
    public async Task<ActionResult<SchoolYear>> CreateSchoolYear(SchoolYear schoolYear)
    {
        schoolYear.Id = Guid.NewGuid();

        // Ensure SchoolYear string is consistent (ex: "2025-2026")
        schoolYear.SchoolYearName = $"{schoolYear.StartDate.Year}-{schoolYear.EndDate.Year}";

        _context.SchoolYears.Add(schoolYear);
        await _context.SaveChangesAsync();

        return Ok(schoolYear);
    }

    // PUT: api/SchoolYear/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSchoolYear(Guid id, SchoolYear updated)
    {
        var existing = await _context.SchoolYears.FirstOrDefaultAsync(y => y.Id == id);
        if (existing == null) return NotFound();

        existing.StartDate = updated.StartDate;
        existing.EndDate = updated.EndDate;
        existing.SchoolYearName = $"{updated.StartDate.Year}-{updated.EndDate.Year}";

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/SchoolYear/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchoolYear(Guid id)
    {
        var existing = await _context.SchoolYears.FirstOrDefaultAsync(y => y.Id == id);
        if (existing == null) return NotFound();

        _context.SchoolYears.Remove(existing);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
