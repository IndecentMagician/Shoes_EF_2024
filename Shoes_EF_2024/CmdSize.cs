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
using System.Drawing;

namespace Shoes_EF_2024.Consola
{
    internal class CmdSize
    {
        private static IServiceProvider? servProvider;
        public static void Iniciar()
        {
            servProvider = DI.ConfigurarServicios();
            Title = "SIZES";
            IniciarMenu();
        }

        private static void IniciarMenu()
        {

            string Prompt = "Menu > Sizes:";
            string[] Opciones = { "Ver Talles", "Agregar un Talle", "Editar un Talle", "Eliminar un Talle", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewSize();
                    break;
                case 1:
                    AddSize();
                    break;
                case 2:
                    EditSize();
                    break;
                case 3:
                    DeleteSize();
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
            var servicio = servProvider?.GetService<IServiceSizes>();
            var sizes = servicio?.GetSizes();

            Console.WriteLine("Listado de Talles ");

            var tabla = new ConsoleTable("ID", "Talles");
            if (sizes != null)
            {
                foreach (var item in sizes)
                {
                    tabla.AddRow(item.SizeId, item.sizeNumber);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {sizes?.Count.ToString()}");
            Console.ResetColor();
        }
         
        public static void ViewSize()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddSize()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceSizes>();

            Console.WriteLine("Agregar Un talle");

            var sizeNumber = ConsoleExtensions.ReadDecimal("Ingrese el número del talle: ");

            var size = new Sizes
            {
                sizeNumber = sizeNumber
            };

            if (servicio != null)
            {
                if (!servicio.Existe(size))
                {
                    servicio.Guardar(size);
                    Console.WriteLine("Talle agregado.");
                }
                else
                {
                    Console.WriteLine("El talle que desea ingresar ya existe.");
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

        public static void EditSize()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceSizes>();
            Console.WriteLine("Editar un Talle: ");
            ViewList();

            var id = ConsoleExtensions.ReadInt("Ingrese el ID del Talle a editar: ", 0, 9999);
            var _size = servicio?.GetSizePorId(id);
            if (_size != null)
            {
                Console.WriteLine($"Talle a editar: {_size.sizeNumber}");
                var newSize = ConsoleExtensions.ReadDecimal("Ingrese el nuevo Talle: ");
                _size.sizeNumber = newSize;
                servicio?.Guardar(_size);
                Console.WriteLine("Se edito el Talle correctamente.");
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

        public static void DeleteSize()
        {
            Clear();
            //codigo

            Console.WriteLine("Elimar Talle");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID del talle que desea eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceSizes>();
                var size = servicio?.GetSizePorId(id);

                if (size != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.EstaRelacionado(size))
                        {
                            servicio.Eliminar(size);
                            Console.WriteLine("Registro eliminado satisfactoriamente.");

                        }
                        else
                        {
                            Console.WriteLine("El registro no puede ser eliminado porque se encuentra relacionado.");
                        }

                    }
                    else
                    {
                        throw new Exception("Servicio no disponible.");
                    }
                }
                else
                {
                    Console.WriteLine("El registro que desea eliminar no existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }

            Thread.Sleep(2000);
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }
    }
}
