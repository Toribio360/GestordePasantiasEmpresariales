namespace GestordePasantíasEmpresariales.Models.ModelsDTOS
{
    public class StudentResponseDto
    {
        public int StudentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}