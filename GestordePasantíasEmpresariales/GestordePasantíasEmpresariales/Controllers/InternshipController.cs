
using Microsoft.AspNetCore.Mvc;
using GestordePasantíasEmpresariales.Data;
using GestordePasantíasEmpresariales.Models.Entities;
using GestordePasantíasEmpresariales.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GestordePasantíasEmpresariales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternshipController : ControllerBase
    {
        private readonly List<Internship> _internships =
            InMemoryDataStore.Internships;

        private readonly List<Student> _students =
            InMemoryDataStore.Students;

        // GET: api/Internship
        [HttpGet]
        public ActionResult<IEnumerable<InternshipResponseDto>> GetAll()
        {
            var internships = _internships.Select(i =>
                new InternshipResponseDto
                {
                    InternshipId = i.InternshipId,
                    Title = i.Title,
                    Description = i.Description,
                    StartDate = i.StartDate,
                    EndDate = i.EndDate,
                    StudentId = i.StudentId,
                    StudentName = GetStudentName(i.StudentId)
                }).ToList();

            return Ok(internships);
        }

        // GET: api/Internship/1
        [HttpGet("{id}")]
        public ActionResult<InternshipResponseDto> GetById(int id)
        {
            var internship = _internships
                .FirstOrDefault(i => i.InternshipId == id);

            if (internship == null)
            {
                return NotFound();
            }

            return Ok(MapToResponse(internship));
        }

        // POST: api/Internship
        [HttpPost]
        public ActionResult<InternshipResponseDto> Create(
            CreateInternshipDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Internship data is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Title is required.");
            }

            if (dto.StartDate == default)
            {
                return BadRequest("StartDate is required.");
            }

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                return BadRequest(
                    "EndDate cannot be earlier than StartDate.");
            }

            var student = _students
                .FirstOrDefault(s => s.StudentId == dto.StudentId);

            if (student == null)
            {
                return BadRequest(
                    "The specified student does not exist.");
            }

            int newId = _internships.Any()
                ? _internships.Max(i => i.InternshipId) + 1
                : 1;

            var internship = new Internship
            {
                InternshipId = newId,
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                StudentId = dto.StudentId,
                Student = student,
                IsActive = true
            };

            _internships.Add(internship);

            var response = MapToResponse(internship);

            return CreatedAtAction(
                nameof(GetById),
                new { id = internship.InternshipId },
                response
            );
        }

        // PUT: api/Internship/1
        [HttpPut("{id}")]
        public IActionResult Update(
            int id,
            CreateInternshipDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Internship data is required.");
            }

            var existing = _internships
                .FirstOrDefault(i => i.InternshipId == id);

            if (existing == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                return BadRequest("Title is required.");
            }

            if (dto.StartDate == default)
            {
                return BadRequest("StartDate is required.");
            }

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                return BadRequest(
                    "EndDate cannot be earlier than StartDate.");
            }

            var student = _students
                .FirstOrDefault(s => s.StudentId == dto.StudentId);

            if (student == null)
            {
                return BadRequest(
                    "The specified student does not exist.");
            }

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.StudentId = dto.StudentId;
            existing.Student = student;

            return NoContent();
        }

        // DELETE: api/Internship/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _internships
                .FirstOrDefault(i => i.InternshipId == id);

            if (existing == null)
            {
                return NotFound();
            }

            _internships.Remove(existing);

            return NoContent();
        }

        // Convertir entidad a DTO de respuesta
        private InternshipResponseDto MapToResponse(
            Internship internship)
        {
            return new InternshipResponseDto
            {
                InternshipId = internship.InternshipId,
                Title = internship.Title,
                Description = internship.Description,
                StartDate = internship.StartDate,
                EndDate = internship.EndDate,
                StudentId = internship.StudentId,
                StudentName = GetStudentName(internship.StudentId)
            };
        }

        private string GetStudentName(int studentId)
        {
            var student = _students
                .FirstOrDefault(s => s.StudentId == studentId);

            return student == null
                ? string.Empty
                : $"{student.Name} {student.Lastname}";
        }
    }
}
