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
namespace Shoes_EF_2024.Consola
{
    internal class CmdSport
    {
        private static IServiceProvider? servProvider;
        public static void Iniciar()
        {
            servProvider = DI.ConfigurarServicios();
            Title = "SHOE";
            IniciarMenu();
        }

        private static void IniciarMenu()
        {

            string Prompt = "Menu > Shoe:";
            string[] Opciones = { "Ver Deportes", "Agregar un Deporte", "Editar un Deporte", "Eliminar un Deporte", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewSports();
                    break;
                case 1:
                    AddSport();
                    break;
                case 2:
                    EditSport();
                    break;
                case 3:
                    DeleteSport();
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
            var servicio = servProvider?.GetService<IServiceSports>();
            var _sport = servicio?.GetSports();

            Console.WriteLine("Listado de Deportes ");

            var tabla = new ConsoleTable("ID", "Deportes");
            if (_sport != null)
            {
                foreach (var item in _sport)
                {
                    tabla.AddRow(item.SportId, item.SportName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {_sport?.Count.ToString()}");
            Console.ResetColor();
        }

        public static void ViewSports()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddSport()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceSports>();

            Console.WriteLine("Agregar Un Deporte: ");
            ViewList();

            var _sportName = ConsoleExtensions.ReadString("Nombre Del Deporte: ");

            var _sport = new Sports
            {
                SportName = _sportName
            };

            if (servicio != null)
            {
                if (!servicio.Existe(_sport))
                {
                    servicio.Guardar(_sport);
                    Console.WriteLine("Deporte Agregado");
                }
                else
                {
                    Console.WriteLine("El Deporte que desea ingresar ya existe.");
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

        public static void EditSport()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceSports>();
            Console.WriteLine("Editar un Deporte: ");
            ViewList();

            var id = ConsoleExtensions.ReadInt("Ingrese el ID de la Marca a editar: ", 0, 9999);
            var _sport = servicio?.GetSportPorId(id);
            if (_sport != null)
            {
                Console.WriteLine($"Marca a editar: {_sport.SportName}");
                var newSport = ConsoleExtensions.ReadString("Ingrese El Deporte: ");
                _sport.SportName = newSport;
                servicio?.Guardar(_sport);
                Console.WriteLine("Se edito el Deporte correctamente.");
            }
            else
            {
                Console.WriteLine("No se encuentra el ID Ingresado.");
            }
            Thread.Sleep(3000);

            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void DeleteSport()
        {
            Clear();
            //codigo

            Console.WriteLine("Elimar Deporte");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID a Eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceSports>();
                var _sport = servicio?.GetSportPorId(id);
                if (_sport != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.EstaRelacionado(_sport))
                        {
                            servicio.Eliminar(_sport);
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
