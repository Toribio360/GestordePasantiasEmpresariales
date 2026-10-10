
using System;

namespace GestordePasantíasEmpresariales.Models.DTOs
{
    public class InternshipResponseDto
    {
        public int InternshipId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;
    }
}
