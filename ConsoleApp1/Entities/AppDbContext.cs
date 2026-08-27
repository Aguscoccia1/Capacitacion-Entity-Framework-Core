using Capacitacion;
using Microsoft.EntityFrameworkCore;

namespace TuProyecto 
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CursoDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        // El DbSet representa la tabla cursos en SQL Server 
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Nombre = "curso 1" },
                new Curso { Id = 2, Nombre = "curso 2" }
            );
        }
    }
}