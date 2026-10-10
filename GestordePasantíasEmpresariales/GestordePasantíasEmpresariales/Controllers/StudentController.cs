

using GestordePasantíasEmpresariales.Data;
using GestordePasantíasEmpresariales.Models.Entities;
using GestordePasantíasEmpresariales.Models.ModelsDTOS;
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
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetAll()
        {
            var students = await _context.Students
                .AsNoTracking()
                .Select(s => new StudentResponseDto
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Lastname = s.Lastname,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetById(int id)
        {
            var student = await _context.Students
                .AsNoTracking()
                .Where(s => s.StudentId == id)
                .Select(s => new StudentResponseDto
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Lastname = s.Lastname,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> Create(
            CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Lastname = dto.Lastname,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                IsActive = true
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var response = new StudentResponseDto
            {
                StudentId = student.StudentId,
                Name = student.Name,
                Lastname = student.Lastname,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                IsActive = student.IsActive
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = student.StudentId },
                response
            );
        }

        // PUT: api/Student/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateStudentDto dto)
        {
            var existing = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (existing == null)
                return NotFound();

            existing.Name = dto.Name;
            existing.Lastname = dto.Lastname;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.Address = dto.Address;
            existing.IsActive = dto.IsActive;

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
