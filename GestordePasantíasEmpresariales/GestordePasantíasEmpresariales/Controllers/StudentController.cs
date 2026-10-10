
using GestordePasantíasEmpresariales.Data;
using GestordePasantíasEmpresariales.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GestordePasantíasEmpresariales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private List<Student> _students => InMemoryDataStore.Students;
      

        // GET: api/Student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            return Ok(_students);
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            var student = _students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            if (student == null ||
                string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Lastname))
            {
                return BadRequest("Name and Lastname are required.");
            }

            int newId = _students.Any()
                ? _students.Max(s => s.StudentId) + 1
                : 1;

            student.StudentId = newId;
            student.IsActive = true;

            _students.Add(student);

            return CreatedAtAction(
                nameof(GetById),
                new { id = student.StudentId },
                student
            );
        }

        // PUT: api/Student/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Lastname))
            {
                return BadRequest("Name and Lastname are required.");
            }

            var existing = _students.FirstOrDefault(s => s.StudentId == id);

            if (existing == null)
            {
                return NotFound();
            }

    
            existing.Name = student.Name;
            existing.Lastname = student.Lastname;
            existing.Email = student.Email;
            existing.PhoneNumber = student.PhoneNumber;
            existing.IsActive = student.IsActive;

            return NoContent();
        }

        // DELETE: api/Student/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _students.FirstOrDefault(s => s.StudentId == id);

            if (existing == null)
            {
                return NotFound();
            }

            _students.Remove(existing);

            return NoContent();
        }
    }
}
