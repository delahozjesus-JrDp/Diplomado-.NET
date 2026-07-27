namespace Ejercicio_02_POO.Models
{
    public class Categoria
    {
        public string Nombre { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        // Constructor vacío
        public Categoria()
        {
        }

        // Constructor con parámetros
        public Categoria(string nombre, string color, string descripcion)
        {
            Nombre = nombre;
            Color = color;
            Descripcion = descripcion;
        }
    }
}