

using GestordePasantíasEmpresariales.Data;
using GestordePasantíasEmpresariales.Models.Entities;
using GestordePasantíasEmpresariales.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestordePasantíasEmpresariales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternshipController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InternshipController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Internship
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternshipResponseDto>>> GetAll()
        {
            var internships = await _context.Internships
                .AsNoTracking()
                .Select(i => new InternshipResponseDto
                {
                    InternshipId = i.InternshipId,
                    Title = i.Title,
                    Description = i.Description,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    StudentId = i.StudentId,
                    StudentName = i.Student != null
                        ? i.Student.Name + " " + i.Student.Lastname
                        : string.Empty
                })
                .ToListAsync();

            return Ok(internships);
        }

        // GET: api/Internship/1
        [HttpGet("{id}")]
        public async Task<ActionResult<InternshipResponseDto>> GetById(int id)
        {
            var internship = await _context.Internships
                .AsNoTracking()
                .Where(i => i.InternshipId == id)
                .Select(i => new InternshipResponseDto
                {
                    InternshipId = i.InternshipId,
                    Title = i.Title,
                    Description = i.Description,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    StudentId = i.StudentId,
                    StudentName = i.Student != null
                        ? i.Student.Name + " " + i.Student.Lastname
                        : string.Empty
                })
                .FirstOrDefaultAsync();

            if (internship == null)
                return NotFound();

            return Ok(internship);
        }

        // POST: api/Internship
        [HttpPost]
        public async Task<ActionResult<InternshipResponseDto>> Create(
            CreateInternshipDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            if (dto.StartDate == default)
                return BadRequest("StartDate is required.");

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                return BadRequest(
                    "EndDate cannot be earlier than StartDate.");
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == dto.StudentId);

            if (student == null)
                return BadRequest("The specified student does not exist.");

            var internship = new Internship
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StudentId = dto.StudentId,
                IsActive = true
            };

            _context.Internships.Add(internship);
            await _context.SaveChangesAsync();

            var response = new InternshipResponseDto
            {
                InternshipId = internship.InternshipId,
                Title = internship.Title,
                Description = internship.Description,
                StartDate = internship.StartDate,
                EndDate = internship.EndDate,
                StudentId = internship.StudentId,
                StudentName = $"{student.Name} {student.Lastname}"
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = internship.InternshipId },
                response
            );
        }

        // PUT: api/Internship/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateInternshipDto dto)
        {
            var existing = await _context.Internships
                .FirstOrDefaultAsync(i => i.InternshipId == id);

            if (existing == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required.");

            if (dto.StartDate == default)
                return BadRequest("StartDate is required.");

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                return BadRequest(
                    "EndDate cannot be earlier than StartDate.");
            }

            var studentExists = await _context.Students
                .AnyAsync(s => s.StudentId == dto.StudentId);

            if (!studentExists)
                return BadRequest("The specified student does not exist.");

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.StudentId = dto.StudentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Internship/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Internships
                .FirstOrDefaultAsync(i => i.InternshipId == id);

            if (existing == null)
                return NotFound();

            _context.Internships.Remove(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
