
using System;
using System.ComponentModel.DataAnnotations;

namespace GestordePasantíasEmpresariales.Models.DTOs
{
    public class UpdateInternshipDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
