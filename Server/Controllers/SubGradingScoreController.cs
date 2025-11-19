using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubGradingScoreController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubGradingScoreController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SubGradingScore
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubGradingScore>>> GetSubGradingScores()
    {
        return await _context.SubGradingScores.ToListAsync();
    }

    // GET: api/SubGradingScore/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SubGradingScore>> GetSubGradingScore(Guid id)
    {
        var score = await _context.SubGradingScores.FindAsync(id);

        if (score == null)
            return NotFound();

        return score;
    }

    // GET: api/SubGradingScore/ByItem/{subGradingItemId}
    [HttpGet("ByItem/{subGradingItemId}")]
    public async Task<ActionResult<IEnumerable<SubGradingScore>>> GetScoresByItem(Guid subGradingItemId)
    {
        var scores = await _context.SubGradingScores
            .Where(s => s.SubGradingItemId == subGradingItemId)
            .ToListAsync();

        return Ok(scores);
    }

    // GET: api/SubGradingScore/ByStudent/{studentId}
    [HttpGet("ByStudent/{studentId}")]
    public async Task<ActionResult<IEnumerable<SubGradingScore>>> GetScoresByStudent(Guid studentId)
    {
        var scores = await _context.SubGradingScores
            .Where(s => s.StudentId == studentId)
            .ToListAsync();

        return Ok(scores);
    }


    // POST: api/SubGradingScore
    [HttpPost]
    public async Task<ActionResult<SubGradingScore>> PostSubGradingScore(SubGradingScore score)
    {
        _context.SubGradingScores.Add(score);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSubGradingScore), new { id = score.Id }, score);
    }

    // PUT: api/SubGradingScore/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubGradingScore(Guid id, SubGradingScore score)
    {
        if (id != score.Id)
            return BadRequest();

        _context.Entry(score).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubGradingScoreExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/SubGradingScore/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubGradingScore(Guid id)
    {
        var score = await _context.SubGradingScores.FindAsync(id);
        if (score == null)
            return NotFound(); 

        _context.SubGradingScores.Remove(score);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubGradingScoreExists(Guid id)
    {
        return _context.SubGradingScores.Any(e => e.Id == id);
    }

    // POST: api/SubGradingScore/Bulk
    [HttpPost("Bulk")]
    public async Task<IActionResult> PostBulkScores(List<SubGradingScore> scores)
    {
        if (scores == null || !scores.Any())
            return BadRequest("No scores provided.");

        _context.SubGradingScores.AddRange(scores);
        await _context.SaveChangesAsync();

        return Ok(scores.Count);
    }

    // POST: api/SubGradingScore/DeleteAndInsertAll
    [HttpPost("DeleteAndInsertAll")]
    public async Task<IActionResult> DeleteAndInsertAll([FromBody] DeleteAndInsertScoresRequest request)
    {
        if (request == null || request.Scores == null)
            return BadRequest("Invalid request.");

        // Wrap in a transaction for safety
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Delete all scores for this SubGradingItem
            var oldScores = _context.SubGradingScores
                .Where(s => s.SubGradingItemId == request.SubGradingItemId);

            _context.SubGradingScores.RemoveRange(oldScores);
            await _context.SaveChangesAsync();

            // Insert the new scores
            if (request.Scores.Any())
            {
                _context.SubGradingScores.AddRange(request.Scores);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
            return Ok(new { Inserted = request.Scores.Count });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest($"Error during replace: {ex.Message}");
        }
    }

    public class DeleteAndInsertScoresRequest
    {
        public Guid SubGradingItemId { get; set; }
        public List<SubGradingScore> Scores { get; set; } = new();
    }


}
