using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAttendance([FromBody] List<Attendance> records)
        {
            if (records == null || !records.Any())
                return BadRequest("No attendance records provided.");

            foreach (var record in records)
            {
                // Prevent duplicate records for same student/date
                var existing = await _context.Attendances
                    .FirstOrDefaultAsync(a =>
                        a.StudentId == record.StudentId &&
                        a.AttendanceDate == record.AttendanceDate);

                if (existing != null)
                {
                    existing.IsPresent = record.IsPresent;
                    existing.Remarks = record.Remarks;
                }
                else
                {
                    _context.Attendances.Add(record);
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { Count = records.Count });
        }

        [HttpGet("{sectionId:guid}/{date}")]
        public async Task<ActionResult<List<Attendance>>> GetAttendance(Guid sectionId, DateTime date)
        {
            var records = await _context.Attendances
                .Where(a => a.SchoolSectionId == sectionId && a.AttendanceDate.Date == date.Date)
                .ToListAsync();

            return Ok(records);
        }

    }

}
