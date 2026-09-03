using ConsoleApp1.Entities;
using TuProyecto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        // Crea el contexto y aplica migraciones y seed
        using (var context = new AppDbContext())
        {
            // Aplica las migraciones automáticamente
            Console.WriteLine("Verificando base de datos...");
            await context.Database.MigrateAsync();

            // Llama al método de seed para agregar datos iniciales
            await DataSeeder.SeedDataAsync(context);

            Console.WriteLine("Proyectos con sus Tareas Pendientes:");

            // esto trae a los proyectos e incluye solo las tareas que no estén completadas
            var proyectosConTareasPendientes = await context.Proyectos
                .Include(p => p.Tareas.Where(t => !t.Completada)) 
                .ToListAsync();

            //Operacion 1: lectura y filtrado 

            Console.WriteLine();
            Console.WriteLine("Lectura y filtrado de proyectos con tareas pendientes:");

            foreach (var proyecto in proyectosConTareasPendientes)
            {
                Console.WriteLine();
                Console.WriteLine($"Proyecto: {proyecto.Nombre}");

                if (proyecto.Tareas.Any())
                {
                    foreach (var tareaPendiente in proyecto.Tareas) 
                    {
                        Console.WriteLine($"Tarea Pendiente: {tareaPendiente.Titulo} - ID: {tareaPendiente.Id}");
                    }
                }
                else
                {
                    Console.WriteLine("Este proyecto no tiene tareas pendientes");
                }
            }

            // OPERACIÓN 2: actualizacion de estado

            Console.WriteLine();
            Console.WriteLine("Actualización de estado de una tarea");

            Console.WriteLine("¿qué tarea quieres completar?(ingrese el id)");
            int tareaIdACompletar = int.Parse(Console.ReadLine());

            var tareaSeleccionada = await context.Tareas.FindAsync(tareaIdACompletar);

            if (tareaSeleccionada != null)
            {
                Console.WriteLine($"Estado original de '{tareaSeleccionada.Titulo}': Completada = {tareaSeleccionada.Completada}");

                tareaSeleccionada.Completada = true;

                // Guardamos los cambios asincrónicamente en SQL Server
                await context.SaveChangesAsync();

                Console.WriteLine($"Estado actualizado con éxito");
            }
            else
            {
                Console.WriteLine($"No se encontró ninguna tarea con el ID {tareaIdACompletar}, por favor ingrese un ID válido");
            }


            // Operacion 3: Eliminación en Cascada 

            Console.WriteLine();
            Console.WriteLine("Eliminacion en cascada");

            foreach (var proyecto in proyectosConTareasPendientes)
            {
                Console.WriteLine($"Proyecto: {proyecto.Nombre} - ID: {proyecto.Id}");
                foreach (var tarea in proyecto.Tareas)
                {
                    Console.WriteLine($"Tarea Pendiente: {tarea.Titulo} - ID: {tarea.Id}");
                }
            }

            Console.WriteLine("¿Que proyecto quieres eliminar? (ingresa el ID)");
            int idProyectoAEliminar = int.Parse(Console.ReadLine());

            // Buscamos el primer proyecto disponible para borrarlo
            var proyectoParaBorrar = await context.Proyectos.FirstOrDefaultAsync(p => p.Id == idProyectoAEliminar);

            if (proyectoParaBorrar != null)
            {
                int idEliminado = proyectoParaBorrar.Id;
                string nombreEliminado = proyectoParaBorrar.Nombre;

                Console.WriteLine($"Proyecto a eliminar: '{nombreEliminado}' - ID: {idEliminado}");

                // se elimina el proyecto del contexto
                context.Proyectos.Remove(proyectoParaBorrar);

                // Guardamos cambios (esto dispara el borrado en cascada en la base de datos)
                await context.SaveChangesAsync();

                Console.WriteLine($"Proyecto '{nombreEliminado}' eliminado");

                // Se verifica si quedaron tareas asociadas al id del proyecto borrado en la base de datos
                int tareasSueltas = await context.Tareas.CountAsync(t => t.ProyectoId == idEliminado);

                Console.WriteLine($"Tareas en la base de datos vinculadas al ID {idEliminado} = {tareasSueltas}");
            }
            else
            {
                Console.WriteLine("No hay proyectos en la base de datos para eliminar.");
            }

            //El readkey es para que al apretar una tecla pase a lo siguiente, en este caso termina,de lo contrario la terminal se cierra sola.
            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}