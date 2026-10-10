
using GestordePasantíasEmpresariales.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestordePasantíasEmpresariales.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Internship> Internships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Student)
                .WithMany(s => s.Internships)
                .HasForeignKey(i => i.StudentId);
        }
    }
}