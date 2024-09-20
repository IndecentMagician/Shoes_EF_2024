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
    internal class CmdBrand
    {
        private static IServiceProvider? servProvider;
        public static void Iniciar()
        {
            servProvider = DI.ConfigurarServicios();
            Title = "BRANDS";
            IniciarMenu();
        }

        private static void IniciarMenu()
        {

            string Prompt = "Menu > Brands:";
            string[] Opciones = { "Ver Marcas", "Agregar una Marca", "Editar una Marca", "Eliminar una Marca", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewBrands();
                    break;
                case 1:
                    AddBrand();
                    break;
                case 2:
                    EditBrand();
                    break;
                case 3:
                    DeleteBrand();
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
            var servicio = servProvider?.GetService<IServiceBrands>();
            var brands = servicio?.GetLista();

            Console.WriteLine("Listado de Marcas ");

            var tabla = new ConsoleTable("ID", "Marca");
            if (brands != null)
            {
                foreach (var item in brands)
                {
                    tabla.AddRow(item.BrandId, item.BrandName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {brands?.Count.ToString()}");
            Console.ResetColor();
        }

        public static void ViewBrands()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddBrand()
        {
            Clear();
            //codigo
            var servicio = servProvider?.GetService<IServiceBrands>();

            Console.WriteLine("Agregar Una Marca: ");
            ViewList();

            var _brandName = ConsoleExtensions.ReadString("Nombre De la Marca: ");

            var _brand = new Brands
            {
                BrandName = _brandName
            };

            if (servicio != null)
            {
                if (!servicio.existe(_brand))
                {

                    servicio.Guardar(_brand);
                    Console.WriteLine("Marca Agregada");
                }
                else
                {
                    Console.WriteLine("La Marca que desea ingresar ya existe.");
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

        public static void EditBrand()
        {
            Clear();
            //codigo

            var servicio = servProvider?.GetService<IServiceBrands>();
            Console.WriteLine("Editar una Marca: ");
            ViewList();

            var id = ConsoleExtensions.ReadInt("Ingrese el ID de la Marca a editar: ", 0, 9999);
            var brand = servicio?.GetBrandsPorId(id);
            if (brand != null)
            {
                Console.WriteLine($"Marca a editar: {brand.BrandName}");
                var newBrand = ConsoleExtensions.ReadString("Ingrese la nueva Marca: ");
                brand.BrandName = newBrand;
                servicio?.Guardar(brand);
                Console.WriteLine("Se edito la Marca correctamente.");
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

        public static void DeleteBrand()
        {
            Clear();
            //codigo

            Console.WriteLine("Elimar Marca");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID a Eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceBrands>();
                var brand = servicio?.GetBrandsPorId(id);
                if (brand != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.estarelacionado(brand))
                        {
                            servicio.Borrar(brand);
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
