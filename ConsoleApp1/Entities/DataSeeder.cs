using ConsoleApp1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuProyecto;

public static class DataSeeder
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
        //Se verifica si ya existen proyectos en la base de datos para no duplicar datos
        if (!context.Proyectos.Any())
        {
            
            var proyectosNuevos = new List<Proyecto>
            {
                new Proyecto
                {
                    Nombre = "Desarrollo de APP",
                    FechaInicio = DateTime.Now,
                    Tareas = new List<Tarea>
                    {
                        new Tarea { Titulo = "realizar diseño", Completada = true },
                        new Tarea { Titulo = "Crear base de datos ", Completada = false },
                        new Tarea { Titulo = "realizar el testing", Completada = false }
                    }
                },
                new Proyecto
                {
                    Nombre = "Desarrollo de web",
                    FechaInicio = DateTime.Now,
                    Tareas = new List<Tarea>
                    {
                        new Tarea { Titulo = "tarea 1", Completada = true },
                        new Tarea { Titulo = "tarea 2", Completada = true },
                        new Tarea { Titulo = "tarea 3", Completada = false }
                    }
                }
            };


            // Se agrega todo en una sola operación
            await context.Proyectos.AddRangeAsync(proyectosNuevos);
            await context.SaveChangesAsync(); // Guarda de todo de golpe en la base de datos
            Console.WriteLine("DataSeed realizado con exito");
        }
    }
}