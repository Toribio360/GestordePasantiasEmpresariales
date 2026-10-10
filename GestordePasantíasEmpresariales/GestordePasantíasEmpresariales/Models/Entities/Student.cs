namespace GestordePasantíasEmpresariales.Models.Entities
{
    public class Student
    {

        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Internship> Internships { get; set; }
         = new List<Internship>();

    }
}
