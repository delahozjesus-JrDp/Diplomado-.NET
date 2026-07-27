using System;

namespace Ejercicio_02_POO.Models
{
    public class TareaDTO
    {
        public bool TieneVencimiento { get; set; }

        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public Prioridad Prioridad { get; set; }

        public Categoria Categoria { get; set; } = new Categoria();
        
        public bool Completada { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }
}