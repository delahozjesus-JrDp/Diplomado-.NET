using System;

namespace Ejercicio_02_POO.Models
{
    public class Tarea : IExportable
    {
        // Contador para generar IDs automáticamente
        private static int contador = 1;

        // Propiedades
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public Prioridad Prioridad { get; set; }

        public Categoria Categoria { get; set; } = new Categoria();
        
        public bool Completada { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Constructor con parámetros
        public Tarea(string titulo,
                     string descripcion,
                     Prioridad prioridad,
                     Categoria categoria)
        {
            Id = contador++;

            Titulo = titulo;

            Descripcion = descripcion;

            Prioridad = prioridad;

            Categoria = categoria;

            Completada = false;

            FechaCreacion = DateTime.Now;
        }

        // Método virtual para demostrar polimorfismo
        public virtual void MostrarInfo()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Prioridad: {Prioridad}");
            Console.WriteLine($"Categoría: {Categoria.Nombre}");
            Console.WriteLine($"Completada: {Completada}");
            Console.WriteLine($"Fecha creación: {FechaCreacion}");
            Console.WriteLine("-----------------------------------");
        }

        // Implementación de la interfaz
        public string Exportar()
        {
            return $"{Id}|{Titulo}|{Prioridad}|{Completada}";
        }
    }
}