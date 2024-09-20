using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoes_EF_2024.Shared
{
    public static class ConsoleExtensions
    {
        public static string ReadString(string message)
        {
            string? stringVar = string.Empty;
            while (true)
            {
                Console.Write(message);
                stringVar = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringVar))
                {
                    Console.WriteLine("Debe ingresar algo.");
                }
                else if (!IsAlphaNumeric(stringVar))
                {
                    Console.WriteLine("Debe ingresar solo letras.");
                }
                else
                {
                    break;
                }
            }
            return stringVar;
        }

        private static bool IsAlphaNumeric(string input)
        {
            return input.All(c => char.IsLetterOrDigit(c) || c == ' ');
        }

        public static int ReadInt(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;

                    }
                    else
                    {
                        Console.WriteLine($"----------");

                        Console.WriteLine($" Selección fuera de rango ({min}-{max})");
                        Console.WriteLine($"----------");

                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número entero válido.");
                }
            }
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"----------");
                    Console.WriteLine("Por favor, ingrese un número decimal válido.");
                    Console.WriteLine($"----------");

                }
            }
        }
        public static void Enter()
        {
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();

        }
    }
}
