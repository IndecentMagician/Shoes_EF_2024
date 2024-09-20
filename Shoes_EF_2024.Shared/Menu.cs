using static System.Console;

namespace Shoes_EF_2024.Shared
{
    public class Menu
    {
        private int _Seleccion;
        private string[] _Opciones;
        private string _Prompt;

        public Menu(string[] opciones, string prompt)
        {
            this._Opciones = opciones;
            this._Prompt = prompt;
            this._Seleccion = 0;
        }

        public void MenuArmado()
        {
            WriteLine($@"
     ░ Utilizar las flechas para desplazarse
     ░ Presione Enter Para Ejecutar
     ░ {_Prompt} .

");

            for (int i = 0; i < _Opciones.Length; i++)
            {
                string Punto;
                string Opcion = _Opciones[i];
                if (i == _Seleccion)
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Magenta;
                    Punto = "*";
                }
                else
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Magenta;
                    Punto = " ";
                }
                WriteLine($"{Punto} <<:[ {Opcion} ]:>>");

            }
            ResetColor();

        }


        public int Iniciar()
        {
            ConsoleKey Presionada;
            do
            {
                Clear();
                MenuArmado();
                ConsoleKeyInfo InfoKey = ReadKey(true);
                Presionada = InfoKey.Key;
                if (Presionada == ConsoleKey.UpArrow)
                {
                    _Seleccion--;
                    if (_Seleccion==-1)
                    {
                        _Seleccion = _Opciones.Length - 1;
                    }
                }
                else if (Presionada == ConsoleKey.DownArrow)
                {
                    _Seleccion++;
                    if (_Seleccion == _Opciones.Length)
                    {
                        _Seleccion = 0;
                    }
                }
            } while (Presionada != ConsoleKey.Enter);
            return _Seleccion;

        }

    }
}
