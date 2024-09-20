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
    internal class CmdShoe
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
            string[] Opciones = { "Ver zapatillas", "Agregar zapatilla", "Editar Zapatilla", "Eliminar Zapatilla", "Atras" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    ViewShoes();
                    break;
                case 1:
                    AddShoe();
                    break;
                case 2:
                    EditShoe();
                    break;
                case 3:
                    DeleteShoe();
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





        public static void ViewShoes()
        {
            Clear();
            //codigo
            ViewList();
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void AddShoe()
        {
            Clear();
            //codigo

            Console.WriteLine("Agregar una Zapatilla");

            var servicio = servProvider?.GetService<IServiceShoes>();
            var serviciomarca = servProvider?.GetService<IServiceBrands>();
            var serviciodeportes = servProvider?.GetService<IServiceSports>();
            var serviciogeneros = servProvider?.GetService<IServiceGenres>();
            var serviciocolores = servProvider?.GetService<IServiceColors>();


            var model = ConsoleExtensions.ReadString("Ingrese el modelo de la Zapatilla: ");
            var description = ConsoleExtensions.ReadString("Ingrese la descripcion de la Zapatilla: ");
            var precio = ConsoleExtensions.ReadDecimal("Ingrese el precio de la Zapatilla: ");

            var marca = ValidarMarca(serviciomarca);
            var deporte = ValidarDeporte(serviciodeportes);
            var genero = ValidarGenero(serviciogeneros);
            var colorId = ValidarColor(serviciocolores);

            var shoe = new Shoes
            {
                BrandId = marca,
                SportId = deporte,
                GenreId = genero,
                Model = model,
                Description = description,
                Price = precio,
                ColorID=colorId
            };

            if (servicio != null)
            {
                if (!servicio.Existe(shoe))
                {
                    servicio.Guardar(shoe);
                    Console.WriteLine("Zapato agregado satisfactoriamente.");
                }
                else
                {
                    Console.WriteLine("El zapato que desea ingresar ya existe.");
                }
            }
            else
            {
                Console.WriteLine("Servicio no disponible");
            }

            Thread.Sleep(1000);
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        public static void EditShoe()
        {
            Clear();
            //codigo
            var servicio = servProvider?.GetService<IServiceShoes>();

            Console.WriteLine("Editar una Zapatilla: ");
            ViewList();
            var id = ConsoleExtensions.ReadInt("ID del zapatilla a editar: ", 0, 9999);
            var shoe = servicio?.GetShoePorId(id);

            if (shoe != null)
            {
                Console.WriteLine($"Zapatilla a editar: ID: {shoe.ShoeId}, Marca: , Modelo:{shoe.Model}, Descripcion: {shoe.Description} ");
                var model = ConsoleExtensions.ReadString("Ingrese el modelo de la zapatilla: ");
                var description = ConsoleExtensions.ReadString("Ingrese la descripcion de la zapatilla: ");
                var precio = ConsoleExtensions.ReadDecimal("Ingrese el precio de la zapatilla: ");
                ViewListBrands();
                var marca = ConsoleExtensions.ReadInt("Ingrese el ID de la marca de la zapatilla: ", 0, 9999);
                ViewListSports();
                var deporte = ConsoleExtensions.ReadInt("Ingrese el ID del deporte de la zapatilla: ", 0, 9999);
                ViewListGenre();
                var genero = ConsoleExtensions.ReadInt("Ingrese el ID del genero de la zapatilla: ", 0, 9999);

                shoe.Model = model;
                shoe.Description = description;
                shoe.Price = precio;
                shoe.BrandId = marca;
                shoe.SportId = deporte;
                shoe.GenreId = genero;

                servicio?.Guardar(shoe);
                Console.WriteLine("Zapatilla editada satisfactoriamente.");
            }
            else
            {
                Console.WriteLine("La Zapatilla que desea editar no existe.");
            }
            Thread.Sleep(2000);

            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }

        private static void ViewList()
        {
            var servicio = servProvider?.GetService<IServiceShoes>();
            var shoes = servicio?.GetShoes();

            Console.WriteLine("Listado De Zapatillas");

            var tabla = new ConsoleTable("ID", "MARCA", "MODELO", "DESCRIPCION", "DEPORTE", "COLOR", "GENERO", "PRECIO");

            if (shoes != null)
            {
                foreach (var s in shoes)
                {
                    tabla.AddRow(s.ShoeId, s.brand.BrandName, s.Model, s.Description, s.sport.SportName, s.color.ColorName,
                        s.genre.GenreName, s.Price);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {servicio?.GetCantidad()}");
        }

        public static void DeleteShoe()
        {
            Clear();
            //codigo
            Console.WriteLine("Eliminar Una zapatilla");
            ViewList();
            var id = ConsoleExtensions.ReadInt("Ingrese el ID de la Zapatilla que desea eliminar: ", 0, 9999);
            try
            {
                var servicio = servProvider?.GetService<IServiceShoes>();
                var shoe = servicio?.GetShoePorId(id);

                if (shoe != null)
                {
                    if (servicio != null)
                    {
                        if (!servicio.EstaRelacionado(shoe))
                        {
                            servicio.Eliminar(shoe);
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

            Thread.Sleep(5000);
            WriteLine("Presione una tecla para regresar <<<< ");
            ReadLine();
            IniciarMenu();
        }
        // view lists


        private static void ViewListColors()
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
            Console.WriteLine($"Cantidad: {colors.Count.ToString()}");
        }

        private static void ViewListSports()
        {
            var servicio = servProvider?.GetService<IServiceSports>();
            var _sports = servicio?.GetSports();

            Console.WriteLine("Listado de Deportes ");

            var tabla = new ConsoleTable("ID", "Deporte");
            if (_sports != null)
            {
                foreach (var item in _sports)
                {
                    tabla.AddRow(item.SportId, item.SportName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {_sports?.Count.ToString()}");
            Console.ResetColor();
        }

        private static void ViewListBrands()
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


        private static void ViewListGenre()
        {
            var servicio = servProvider?.GetService<IServiceGenres>();
            var _genre = servicio?.GetGenres();

            Console.WriteLine("Listado de Zapatillas ");

            var tabla = new ConsoleTable("ID", "Zap");
            if (_genre != null)
            {
                foreach (var item in _genre)
                {
                    tabla.AddRow(item.GenreId, item.GenreName);
                }
            }
            tabla.Options.EnableCount = false;
            tabla.Write();
            Console.WriteLine($"Cantidad: {_genre?.Count.ToString()}");
            Console.ResetColor();
        }


        //VAL

        public static int ValidarMarca(IServiceBrands serviciomarca)
        {
            Brands? marca;

            var valido = true;
            do
            {
                Console.Clear();
                ViewListBrands();
                List<Brands> listamarcas = serviciomarca.GetLista();
                var marcaId = ConsoleExtensions.ReadInt("Ingrese el ID de la marca del zapato: ", 0, 9999);
                marca = serviciomarca?.GetBrandsPorId(marcaId);
                if (marca != null && serviciomarca != null)
                {
                    if (listamarcas.Any(b => b.BrandId == marca.BrandId))
                    {
                        valido = false;
                    }
                }
                else
                {
                    Console.WriteLine("ID de la marca no válido. Inténtelo nuevamente.");
                    valido = true;
                }

            } while (valido);

            return marca.BrandId;
        }
        public static int ValidarDeporte(IServiceSports serviciodeportes)
        {
            Sports? deporte;
            var valido = false;
            do
            {
                Console.Clear();
                ViewListSports();
                List<Sports> listadeportes = serviciodeportes.GetSports();
                var deporteId = ConsoleExtensions.ReadInt("Ingrese el ID del deporte del zapato: ", 0, 9999);
                deporte = serviciodeportes?.GetSportPorId(deporteId);
                if (deporte != null && serviciodeportes != null)
                {
                    if (listadeportes.Any(d => d.SportId == deporte.SportId))
                    {
                        valido = true;
                    }
                    else
                    {
                        Console.WriteLine("ID del deporte no válido. Inténtelo nuevamente.");
                        valido = false;
                    }
                }
            } while (!valido);

            return deporte.SportId;
        }
        public static int ValidarGenero(IServiceGenres serviciogeneros)
        {
            Genre? genero;
            var valido = false;
            do
            {
                Console.Clear();
                ViewListGenre();
                List<Genre> listageneros = serviciogeneros.GetGenres();
                var generoId = ConsoleExtensions.ReadInt("Ingrese el ID del genero del zapato: ", 0, 9999);
                genero = serviciogeneros?.GetGenrePorId(generoId);
                if (genero != null && serviciogeneros != null)
                {
                    if (listageneros.Any(g => g.GenreId == genero.GenreId))
                    {
                        valido = true;
                    }
                    else
                    {
                        Console.WriteLine("ID del género no válido. Inténtelo nuevamente.");
                        valido = false;
                    }
                }
            } while (!valido);

            return genero.GenreId;
        }
        public static int ValidarColor(IServiceColors serviciocolores)
        {
            Colors? color;
            var valido = false;
            do
            {
                Console.Clear();
                ViewListColors();
                List<Colors> listacolores = serviciocolores.GetLista();
                var colorId = ConsoleExtensions.ReadInt("Ingrese el ID del color del zapato: ", 0, 9999);
                color = serviciocolores?.GetColorsPorId(colorId);
                if (color != null && serviciocolores != null)
                {
                    if (listacolores.Any(c => c.ColorId == color.ColorId))
                    {
                        valido = true;
                    }
                    else
                    {
                        Console.WriteLine("ID del color no válido. Inténtelo nuevamente.");
                        valido = false;
                    }
                }
            } while (!valido);

            return color.ColorId;
        }
    }
}
