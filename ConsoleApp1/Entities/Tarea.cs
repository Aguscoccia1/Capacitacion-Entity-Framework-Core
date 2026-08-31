using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Entities
{
    public class Tarea
    {
        private int id;
        private string titulo;
        private bool completada;
        private int proyectoId;
        private Proyecto proyecto;

        public int Id { get { return this.id; } set { this.id = value; } }
        public string Titulo { get { return this.titulo; } set { this.titulo = value; } }
        public bool Completada { get { return this.completada; } set { this.completada = value; } }
        public int ProyectoId { get { return this.proyectoId; } set { this.proyectoId = value; } }
        public Proyecto Proyecto { get { return this.proyecto; } set { this.proyecto = value; } }

    }
}
