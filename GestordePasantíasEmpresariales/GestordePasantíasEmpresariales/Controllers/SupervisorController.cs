
using Microsoft.AspNetCore.Mvc;
using GestordePasantíasEmpresariales.Models.Entities;
using System.Linq;
using System.Collections.Generic;

namespace GestordePasantíasEmpresariales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private static readonly List<Supervisor> _supervisors = new();

        // GET: api/Supervisor
        [HttpGet]
        public ActionResult<IEnumerable<Supervisor>> GetAll()
        {
            return Ok(_supervisors);
        }

        // GET: api/Supervisor/1
        [HttpGet("{id}")]
        public ActionResult<Supervisor> GetById(int id)
        {
            var supervisor = _supervisors
                .FirstOrDefault(s => s.SupervisorId == id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return Ok(supervisor);
        }

        // POST: api/Supervisor
        [HttpPost]
        public ActionResult<Supervisor> Create(Supervisor supervisor)
        {
            if (supervisor == null ||
                string.IsNullOrWhiteSpace(supervisor.Name) ||
                string.IsNullOrWhiteSpace(supervisor.Lastname) ||
                string.IsNullOrWhiteSpace(supervisor.Email))
            {
                return BadRequest(
                    "Name, Lastname and Email are required."
                );
            }

            int newId = _supervisors.Any()
                ? _supervisors.Max(s => s.SupervisorId) + 1
                : 1;

            supervisor.SupervisorId = newId;
            supervisor.IsActive = true;

            _supervisors.Add(supervisor);

            return CreatedAtAction(
                nameof(GetById),
                new { id = supervisor.SupervisorId },
                supervisor
            );
        }

        // PUT: api/Supervisor/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Supervisor supervisor)
        {
            if (string.IsNullOrWhiteSpace(supervisor.Name) ||
                string.IsNullOrWhiteSpace(supervisor.Lastname) ||
                string.IsNullOrWhiteSpace(supervisor.Email))
            {
                return BadRequest(
                    "Name, Lastname and Email are required."
                );
            }

            var existing = _supervisors
                .FirstOrDefault(s => s.SupervisorId == id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = supervisor.Name;
            existing.Lastname = supervisor.Lastname;
            existing.Email = supervisor.Email;
            existing.PhoneNumber = supervisor.PhoneNumber;
            existing.CompanyId = supervisor.CompanyId;
            existing.Position = supervisor.Position;
            existing.HireDate = supervisor.HireDate;
            existing.IsActive = supervisor.IsActive;

            return NoContent();
        }

        // DELETE: api/Supervisor/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _supervisors
                .FirstOrDefault(s => s.SupervisorId == id);

            if (existing == null)
            {
                return NotFound();
            }

            _supervisors.Remove(existing);

            return NoContent();
        }
    }
}
