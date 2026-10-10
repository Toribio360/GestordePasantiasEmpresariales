
using System;
using System.ComponentModel.DataAnnotations;

namespace GestordePasantíasEmpresariales.Models.Entities
{
    public class Internship
    {
        public int InternshipId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Clave foránea
        public int StudentId { get; set; }

       
        public Student? Student { get; set; }
    }
}
