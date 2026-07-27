using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Ejercicio_02_POO.Models;

namespace Ejercicio_02_POO.Services
{
    public class GestorTareas
    {
        private List<Tarea> tareas;

        public GestorTareas()
        {
            tareas = new List<Tarea>();
        }

        public void Agregar(Tarea tarea)
        {
            tareas.Add(tarea);
        }

        public List<Tarea> ObtenerTodas()
        {
            return tareas;
        }

        public void Completar(int id)
        {
            foreach (Tarea tarea in tareas)
            {
                if (tarea.Id == id)
                {
                    tarea.Completada = true;
                    Console.WriteLine("Tarea marcada como completada.");
                    return;
                }
            }

            Console.WriteLine("No se encontró la tarea.");
        }

        public void Eliminar(int id)
        {
            Tarea tareaEliminar = null;

            foreach (Tarea tarea in tareas)
            {
                if (tarea.Id == id)
                {
                    tareaEliminar = tarea;
                    break;
                }
            }

            if (tareaEliminar != null)
            {
                tareas.Remove(tareaEliminar);
                Console.WriteLine("Tarea eliminada.");
            }
            else
            {
                Console.WriteLine("No existe la tarea.");
            }
        }

        public List<Tarea> ListarPorCategoria(string categoria)
        {
            List<Tarea> resultado = new List<Tarea>();

            foreach (Tarea tarea in tareas)
            {
                if (tarea.Categoria.Nombre.Equals(categoria,
                    StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(tarea);
                }
            }

            return resultado;
        }

        public List<Tarea> ListarPorPrioridad(Prioridad prioridad)
        {
            List<Tarea> resultado = new List<Tarea>();

            foreach (Tarea tarea in tareas)
            {
                if (tarea.Prioridad == prioridad)
                {
                    resultado.Add(tarea);
                }
            }

            return resultado;
        }

        public List<Tarea> ObtenerVencidas()
        {
            List<Tarea> resultado = new List<Tarea>();

            foreach (Tarea tarea in tareas)
            {
                if (tarea is TareaConVencimiento)
                {
                    TareaConVencimiento tv = (TareaConVencimiento)tarea;

                    if (tv.FechaVencimiento < DateTime.Now)
                    {
                        resultado.Add(tv);
                    }
                }
            }

            return resultado;
        }

        public List<Tarea> ObtenerLista()
        {
            return tareas;
        }

        public void ReemplazarLista(List<Tarea> nuevaLista)
        {
            tareas = nuevaLista;
        }

        public void GuardarEnJSON(string archivo)
        {
            try
            {
                string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(archivo, json);

                Console.WriteLine("Archivo guardado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el archivo.");
                Console.WriteLine(ex.Message);
            }
        }
        public void CargarDeJSON(string archivo)
        {
            try
            {
                if (File.Exists(archivo))
                {
                    string json = File.ReadAllText(archivo);

                    List<Tarea>? lista = JsonSerializer.Deserialize<List<Tarea>>(json);

                    if (lista != null)
                    {
                        tareas = lista;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo.");

                Console.WriteLine(ex.Message);
            }
        }
    }
}