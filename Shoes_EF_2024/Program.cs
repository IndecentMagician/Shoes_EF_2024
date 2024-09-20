using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoes_EF_2024.Consola;
using Shoes_EF_2024.Shared;
using static System.Console;


namespace programShoes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Title = "don-b0$$-co.";
            IniciarMenu();


        }

        private static void IniciarMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLine(@"
        .▄▄ ·   ▄ .▄      ▄▄▄ ..▄▄ · 
        ▐█ ▀.  ██▪▐█▪     ▀▄.▀·▐█ ▀.    EntityFrameworks
        ▄▀▀▀█▄ ██▀▐█ ▄█▀▄ ▐▀▀▪▄▄▀▀▀█▄   MVC - 2024
        ▐█▄▪▐███▌ ▐▀▐█▌.▐▌▐█▄▄▌▐█▄▪▐█
        ▀▀▀▀▀ ▀▀  · ▀█▄▀▪ ▀▀▀  ▀▀▀▀ 
");
            Console.ResetColor();
            Thread.Sleep(2000);
            string Prompt = ".:MENU PRINCIPAL:.";
            string[] Opciones = { "Shoes", "Brands", "Genres", "Sports", "Colors", "Sizes", "About", "Salir" };
            Menu menu = new Menu(Opciones, Prompt);
            int Seleccion = menu.Iniciar();
            switch (Seleccion)
            {
                case 0:
                    EjecutarShoes();
                    break;
                case 1:
                    EjecutarBrands();
                    break;
                case 2:
                    EjecutarGenre();
                    break;
                case 3:
                    EjecutarSports();
                    break;
                case 4:
                    EjecutarColors();
                    break;
                case 5:
                    EjecutarSizes();
                    break;
                case 6:
                    About();
                    break;
                case 7:
                    Salir();
                    break;
            }

        }
        private static void Salir()
        {
            WriteLine("Presione una tecla para salir");
            ReadLine();
            Environment.Exit(0);
        }

        private static void About()
        {
            Clear();
            WriteLine(@"
           ▄▄▄▄    ▒█████    ██████ ▄▄▄█████▓ ▒█████   ███▄    █ 
           ▓█████▄ ▒██▒  ██▒▒██    ▒ ▓  ██▒ ▓▒▒██▒  ██▒ ██ ▀█   █ 
           ▒██▒ ▄██▒██░  ██▒░ ▓██▄   ▒ ▓██░ ▒░▒██░  ██▒▓██  ▀█ ██▒
           ▒██░█▀  ▒██   ██░  ▒   ██▒░ ▓██▓ ░ ▒██   ██░▓██▒  ▐▌██▒
           ░▓█  ▀█▓░ ████▓▒░▒██████▒▒  ▒██▒ ░ ░ ████▓▒░▒██░   ▓██░
           ░▒▓███▀▒░ ▒░▒░▒░ ▒ ▒▓▒ ▒ ░  ▒ ░░   ░ ▒░▒░▒░ ░ ▒░   ▒ ▒ 
           ▒░▒   ░   ░ ▒ ▒░ ░ ░▒  ░ ░    ░      ░ ▒ ▒░ ░ ░░   ░ ▒░
            ░    ░ ░ ░ ░ ▒  ░  ░  ░    ░      ░ ░ ░ ▒     ░   ░ ░ 
            ░          ░ ░        ░               ░ ░           ░ 
                 ░      IG: @BostonJoker                          ");

            ReadLine();
            IniciarMenu();
        }
        private static void EjecutarShoes()
        {
            Clear();
            WriteLine("...");
            CmdShoe.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }
        private static void EjecutarBrands()
        {
            Clear();
            WriteLine("...");
            CmdBrand.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }
        private static void EjecutarGenre()
        {
            Clear();
            WriteLine("...");
            CmdGenre.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }
        private static void EjecutarSports()
        {
            Clear();
            WriteLine("...");
            CmdSport.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }

        private static void EjecutarColors()
        {
            Clear();
            WriteLine("...");
            CmdColor.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }
        private static void EjecutarSizes()
        {
            Clear();
            WriteLine("...");
            CmdSize.Iniciar();
            WriteLine("Presione una tecla para regresar al menu <<<<");
            ReadLine();
            IniciarMenu();

        }
    }
}
