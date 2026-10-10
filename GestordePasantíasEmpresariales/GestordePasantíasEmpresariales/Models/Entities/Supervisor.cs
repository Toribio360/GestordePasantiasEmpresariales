
namespace GestordePasantíasEmpresariales.Models.Entities
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public int CompanyId { get; set; }

        public string Position { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }
    }
}
