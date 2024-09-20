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
    internal class CmdColor
    {
        private static IServiceProvider? servProvider;
        public static void Iniciar()
        {
            servProvider = DI.ConfigurarServicios();
            Title = "COLORS";
            IniciarMenu();
        }

        private static void IniciarMenu()
        {
            
            string Prompt = "Menu > Colors:";
            string[] Opciones = { "Ver Colores", "Agregar un Color", "Editar un Color", "Eliminar un Color", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewColors();
                    break;
                case 1:
                    AddColor();
                    break;
                case 2:
                    EditColor();
                    break;
                case 3:
                    DeleteColor();
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
            var servicio = servProvider?.GetService<IServiceColors>();
            var colors = servicio?.GetLista();

            Console.WriteLine("LListado de Colores ");

            var tabla = new ConsoleTable("ID", "COLOR");
            if (colors != null)
            {
                foreach (var item in colors)
                {
                    tabla.AddRow(item.ColorId, item.ColorName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {colors?.Count.ToString()}");
            Console.ResetColor();
        }

        public static void ViewColors()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddColor()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceColors>();

            Console.WriteLine("Agregar Un Color: ");
            ViewList();

            var colorName = ConsoleExtensions.ReadString("Nombre Del Color: ");

            var color = new Colors
            {
                ColorName = colorName
            };

            if (servicio != null)
            {
                if (!servicio.Existe(color))
                {

                    servicio.Guardar(color);
                    Console.WriteLine("Color Agregado");
                }
                else
                {
                    Console.WriteLine("El Color que desea ingresar ya existe.");
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

        public static void EditColor()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceColors>();
            Console.WriteLine("Editar un Color: ");
            ViewList();

            var id = ConsoleExtensions.ReadInt("Ingrese el ID del Color a editar: ", 0, 9999);
            var color = servicio?.GetColorsPorId(id);
            if (color != null)
            {
                Console.WriteLine($"Color a editar: {color.ColorName}");
                var newColor = ConsoleExtensions.ReadString("Ingrese el nuevo Color: ");
                color.ColorName = newColor;
                servicio?.Guardar(color);
                Console.WriteLine("Se edito el COLOR correctamente.");


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

        public static void DeleteColor()
        {
            Clear();
            //codigo

            Console.WriteLine("Elimar Color");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID a Eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceColors>();
                var color = servicio?.GetColorsPorId(id);
                if (color != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.EstaRelacionado(color))
                        {
                            servicio.Borrar(color);
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
