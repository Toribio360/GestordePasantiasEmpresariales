namespace GestordePasantíasEmpresariales.Models.Entities
{
    public class Student
    {

        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
