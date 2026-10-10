
using System.Collections.Generic;
using GestordePasantíasEmpresariales.Models.Entities;

namespace GestordePasantíasEmpresariales.Data
{
    public static class InMemoryDataStore
    {
        public static List<Student> Students { get; } = new();

        public static List<Internship> Internships { get; } = new();
    }
}
