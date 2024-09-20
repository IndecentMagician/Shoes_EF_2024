using Shoes_EF_2024.Shared;
using ConsoleTables;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Xml.Serialization;
using Shoes_EF_2024.Datos.Migrations;
using Shoes_EF_2024.Entidades;
using Shoes_EF_2024.loc;
using Shoes_EF_2024.Servicios.Interfaces;
using Shoes_EF_2024.Servicios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Shoes_EF_2024.Servicios.Servicios;

namespace Shoes_EF_2024.Consola
{
    internal class CmdGenre
    {
        private static IServiceProvider? servProvider;
        public static void Iniciar()
        {
            servProvider = DI.ConfigurarServicios();
            Title = "GENRE";
            IniciarMenu();
        }

        private static void IniciarMenu()
        {

            string Prompt = "Menu > Genre:";
            string[] Opciones = { "Ver Generos", "Agregar un Genero", "Editar un Genero", "Eliminar un Genero", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewGenres();
                    break;
                case 1:
                    AddGenre();
                    break;
                case 2:
                    EditGenre();
                    break;
                case 3:
                    DeleteGenre();
                    break;
                case 4:
                    Salir();
                    break;
            }

        }

        private static void Salir()
        {
            WriteLine("...");
        }

        private static void ViewList()
        {
            var servicio = servProvider?.GetService<IServiceGenres>();
            var _genres = servicio?.GetGenres();

            Console.WriteLine("Listado de Generos ");

            var tabla = new ConsoleTable("ID", "Genero");
            if (_genres != null)
            {
                foreach (var item in _genres)
                {
                    tabla.AddRow(item.GenreId, item.GenreName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {_genres?.Count.ToString()}");
            Console.ResetColor();
        }

        public static void ViewGenres()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddGenre()
        {
            Clear();
            //codigo
            var servicio = servProvider?.GetService<IServiceGenres>();

            Console.WriteLine("Agregar Un Genero: ");
            ViewList();

            var genreName = ConsoleExtensions.ReadString("Nombre Del Genero: ");

            var genre = new Genre
            {
                GenreName = genreName
            };

            if (servicio != null)
            {
                if (!servicio.Existe(genre))
                {
                    servicio.Guardar(genre);
                    Console.WriteLine("Genero Agregado");
                }
                else
                {
                    Console.WriteLine("El genero que desea ingresar ya existe.");
                }
            }
            else
            {
                Console.WriteLine("Servicio no disponible");
            }

            Thread.Sleep(2000);
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void EditGenre()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceGenres>();
            Console.WriteLine("Editar Genero: ");
            ViewList();

            var id = ConsoleExtensions.ReadInt("Ingrese el ID del Genero a editar: ", 0, 9999);
            var genre = servicio?.GetGenrePorId(id);
            if (genre != null)
            {
                Console.WriteLine($"Genero a editar: {genre.GenreName}");
                var newGenre = ConsoleExtensions.ReadString("Ingrese el nuevo genero: ");
                genre.GenreName = newGenre;
                servicio?.Guardar(genre);
                Console.WriteLine("Se edito el genero correctamente.");


            }
            else
            {
                Console.WriteLine("No se encuentra el ID Genero Ingresado.");
            }
            Thread.Sleep(1000);

            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void DeleteGenre()
        {
            Clear();
            //codigo
            Console.WriteLine("Elimar Genero");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID a Eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceGenres>();
                var genre = servicio?.GetGenrePorId(id);
                if (genre != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.EstaRelacionado(genre))
                        {
                            servicio.Eliminar(genre);
                            Console.WriteLine("Registro eliminado.");
                        }
                        else
                        {
                            Console.WriteLine("El registro esta relacionado, No se puede eliminar");
                        }
                    }
                    else
                    {
                        throw new Exception("El servicio no esta disponible");
                    }
                }
                else
                {
                    Console.WriteLine("El registro que desea eliminar no existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            Thread.Sleep(2000);
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }
    }
}
