using Capacitacion;
using ConsoleApp1.Entities;
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
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>().HasData(
                new Curso { Id = 1, Nombre = "curso 1" },
                new Curso { Id = 2, Nombre = "curso 2" }
            );

            //Configuracion de proyecto
            modelBuilder.Entity<Proyecto>(proyecto =>
            {
                proyecto.HasKey(p => p.Id);
                proyecto.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            });

            //Configuracion de tarea
            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.HasKey(t => t.Id);
                tarea.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(200);

                tarea.HasOne(t => t.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            });

        }
    }
}
