using System;

namespace Ejercicio_02_POO.Models
{
    public class TareaConVencimiento : Tarea
    {
        public DateTime FechaVencimiento { get; set; }

        public int DiasRestantes
        {
            get
            {
                return (FechaVencimiento - DateTime.Now).Days;
            }
        }

        public TareaConVencimiento(
            string titulo,
            string descripcion,
            Prioridad prioridad,
            Categoria categoria,
            DateTime fechaVencimiento)
            : base(titulo, descripcion, prioridad, categoria)
        {
            FechaVencimiento = fechaVencimiento;
        }

        public override void MostrarInfo()
        {
            base.MostrarInfo();

            Console.WriteLine($"Fecha vencimiento: {FechaVencimiento:d}");

            Console.WriteLine($"Días restantes: {DiasRestantes}");
        }
    }
}