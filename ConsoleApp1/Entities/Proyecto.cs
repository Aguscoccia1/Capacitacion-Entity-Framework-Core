using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Entities
{
    public class Proyecto
    {
        private int id;
        private string nombre;
        private DateTime fechaInicio;
        private ICollection<Tarea> tareas;


        public int Id { get { return this.id; } set { this.id = value; } }

        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }

        public DateTime FechaInicio { get { return this.fechaInicio; } set { this.fechaInicio = value; } }
        public ICollection<Tarea> Tareas { get { return this.tareas; } set { this.tareas = value; } }
    }
}
