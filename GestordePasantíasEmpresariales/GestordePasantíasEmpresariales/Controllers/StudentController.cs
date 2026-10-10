
using GestordePasantíasEmpresariales.Data;
using GestordePasantíasEmpresariales.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestordePasantíasEmpresariales.Controllers
{
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
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _context.Students
                .AsNoTracking()
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Lastname))
            {
                return BadRequest(
                    "Name and Lastname are required."
                );
            }

            // SQL Server generará el StudentId automáticamente
            student.StudentId = 0;
            student.IsActive = true;

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = student.StudentId },
                student
            );
        }

        // PUT: api/Student/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Lastname))
            {
                return BadRequest(
                    "Name and Lastname are required."
                );
            }

            var existing = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null)
                return NotFound();

            existing.Name = student.Name;
            existing.Lastname = student.Lastname;
            existing.Email = student.Email;
            existing.PhoneNumber = student.PhoneNumber;
            existing.IsActive = student.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Student/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null)
                return NotFound();

            _context.Students.Remove(existing);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
