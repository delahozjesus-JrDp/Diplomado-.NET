using System;
using System.Collections.Generic;
using Ejercicio_02_POO.Models;
using Ejercicio_02_POO.Services;

namespace Ejercicio_02_POO
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorTareas gestor = new GestorTareas();

            gestor.CargarDeJSON("Data/tareas.json");

            int opcion = 0;

            do
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("      GESTOR DE TAREAS");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Listar todas");
                Console.WriteLine("3. Listar por categoría");
                Console.WriteLine("4. Listar por prioridad");
                Console.WriteLine("5. Marcar como completada");
                Console.WriteLine("6. Mostrar tareas vencidas");
                Console.WriteLine("7. Eliminar tarea");
                Console.WriteLine("8. Exportar JSON");
                Console.WriteLine("9. Salir");
                Console.WriteLine();

                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:

                        Console.Write("Título: ");
                        string titulo = Console.ReadLine()!;

                        Console.Write("Descripción: ");
                        string descripcion = Console.ReadLine()!;

                        Console.WriteLine("Prioridad:");
                        Console.WriteLine("0. Baja");
                        Console.WriteLine("1. Media");
                        Console.WriteLine("2. Alta");
                        Console.WriteLine("3. Crítica");

                        int p = Convert.ToInt32(Console.ReadLine());

                        Prioridad prioridad = (Prioridad)p;

                        Console.Write("Nombre de la categoría: ");
                        string nombreCategoria = Console.ReadLine()!;

                        Console.Write("Color: ");
                        string color = Console.ReadLine()!;

                        Console.Write("Descripción de la categoría: ");
                        string descripcionCategoria = Console.ReadLine()!;

                        Categoria categoria = new Categoria(
                            nombreCategoria,
                            color,
                            descripcionCategoria);

                        Console.Write("¿Tiene fecha de vencimiento? (S/N): ");
                        string respuesta = Console.ReadLine()!;

                        if (respuesta.ToUpper() == "S")
                        {
                            Console.Write("Fecha (yyyy-MM-dd): ");

                            DateTime fecha =
                                Convert.ToDateTime(Console.ReadLine());

                            TareaConVencimiento tarea =
                                new TareaConVencimiento(
                                    titulo,
                                    descripcion,
                                    prioridad,
                                    categoria,
                                    fecha);

                            gestor.Agregar(tarea);
                        }
                        else
                        {
                            Tarea tarea = new Tarea(
                                titulo,
                                descripcion,
                                prioridad,
                                categoria);

                            gestor.Agregar(tarea);
                        }

                        Console.WriteLine();

                        Console.WriteLine("Tarea agregada correctamente.");

                        Console.ReadKey();

                        break;

                    case 2:

                        Console.WriteLine();

                        List<Tarea> lista = gestor.ObtenerTodas();

                        foreach (Tarea tarea in lista)
                        {
                            tarea.MostrarInfo();
                        }

                        Console.ReadKey();

                        break;

                    case 3:

                        Console.Write("Categoría: ");

                        string cat = Console.ReadLine()!;

                        List<Tarea> categorias =
                            gestor.ListarPorCategoria(cat);

                        foreach (Tarea tarea in categorias)
                        {
                            tarea.MostrarInfo();
                        }

                        Console.ReadKey();

                        break;

                    case 4:

                        Console.WriteLine("Prioridad:");

                        Console.WriteLine("0. Baja");
                        Console.WriteLine("1. Media");
                        Console.WriteLine("2. Alta");
                        Console.WriteLine("3. Crítica");

                        int pr = Convert.ToInt32(Console.ReadLine());

                        Prioridad prioridadBuscar =
                            (Prioridad)pr;

                        List<Tarea> prioridades =
                            gestor.ListarPorPrioridad(prioridadBuscar);

                        foreach (Tarea tarea in prioridades)
                        {
                            tarea.MostrarInfo();
                        }

                        Console.ReadKey();

                        break;

                    case 5:

                        Console.Write("ID de la tarea: ");

                        int id = Convert.ToInt32(Console.ReadLine());
                        
                        gestor.Completar(id);

                        Console.ReadKey();

                        break;

                    case 6:

                        List<Tarea> vencidas =
                            gestor.ObtenerVencidas();

                        foreach (Tarea tarea in vencidas)
                        {
                            tarea.MostrarInfo();
                        }

                        Console.ReadKey();

                        break;

                    case 7:

                        Console.Write("ID a eliminar: ");

                        int eliminar =
                            Convert.ToInt32(Console.ReadLine());

                        gestor.Eliminar(eliminar);

                        Console.ReadKey();

                        break;

                    case 8:

                        gestor.GuardarEnJSON("Data/tareas.json");

                        Console.WriteLine();

                        Console.WriteLine("Archivo exportado correctamente.");

                        Console.ReadKey();

                        break;
                    case 9:

                        Console.WriteLine();
                        Console.WriteLine("Saliendo del sistema...");

                        break;

                    default:

                        Console.WriteLine("Opción no válida.");

                        Console.ReadKey();

                        break;
                }

            } while (opcion != 9);

            // Guardar automáticamente al salir
            gestor.GuardarEnJSON("Data/tareas.json");
            Console.WriteLine();
            Console.WriteLine("Los datos fueron guardados correctamente.");

        }
    }
}