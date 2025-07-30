using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Student
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return Ok(_context.Students.ToList());
    }

    // GET: api/Student/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(Guid id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
            return NotFound();

        return student;
    }

    // POST: api/Student
    [HttpPost]
    public async Task<IActionResult> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Ok(student);
    }


    // PUT: api/Student/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(Guid id, Student updatedStudent)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound();

        student.FirstName = updatedStudent.FirstName;
        student.MiddleName = updatedStudent.MiddleName;
        student.LastName = updatedStudent.LastName;
        student.DateOfBirth = updatedStudent.DateOfBirth;
        student.Gender = updatedStudent.Gender;
        student.Address = updatedStudent.Address;
        student.ContactNumber = updatedStudent.ContactNumber;

        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/Student/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(Guid id)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
            return NotFound();

        _context.Students.Remove(student);
        _context.SaveChanges();

        return NoContent();
    }
}
