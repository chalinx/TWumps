using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wunpus
{
    public class GameUI
    {
        static public void Jugabilidad()
        {

            new Personaje(5, 4).dato();
            new Wunpu(5, 5).dato();
            new Viento(5, 6).dato();
            new Flecha(5, 7).dato();
            new Hedor(5, 8).dato();
            new Oro(5, 9).dato();
            new Agujero(5, 10).dato();


        }

        static public void instrucciones()
        {
            ConsoleKeyInfo tecla;

            string titulo = CargarTextoDesdeArchivo("titulo_instruc.txt");
            string movimiento = CargarTextoDesdeArchivo("teclas.txt");
            string objetivo = CargarTextoDesdeArchivo("objetivo.txt");
            string agentes = CargarTextoDesdeArchivo("agentes.txt");

            Efecto_letras2(titulo, 30, 1);
            Efecto_letras2(movimiento, 20, 5);

            Console.SetCursorPosition(35, Console.WindowHeight - 3);
            Console.Write("Presione cualquier tecla para continuar...");
            tecla = Console.ReadKey(true);
            Console.Clear();

            Efecto_letras2(titulo, 30, 1);
            Efecto_letras2(objetivo, 20, 5);

            new Oro(21, 11).dato();
            new Hedor(21, 12).dato();
            new Flecha(21, 13).dato();
            new Viento(21, 14).dato();

            Efecto_letras2(agentes, 20, 16);

            Console.SetCursorPosition(35, Console.WindowHeight - 3);
            Console.Write("Presione cualquier tecla para continuar...");
            tecla = Console.ReadKey(true);
            Console.Clear();
        }

        public static string CargarTextoDesdeArchivo(string rutaArchivo)
        {
            try
            {
                return File.ReadAllText(rutaArchivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo '{rutaArchivo}': {ex.Message}");
                return string.Empty;
            }
        }

        static public void Efecto_letras2(string texto, int x, int y)
        {
            int anchoConsola = Console.WindowWidth; 
            int posicionX = x;                      
            int posicionY = y;                      

            foreach (char c in texto)
            {
                if (c == '\n') 
                {
                    posicionX = x;
                    posicionY++;
                    continue;
                }

                if (posicionX >= anchoConsola - 1)
                {
                    posicionX = x;
                    posicionY++;
                }

                if (posicionY < Console.WindowHeight)
                {
                    Console.SetCursorPosition(posicionX, posicionY);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(c);
                }

                posicionX++;
            }
        }
        static public void intro(string a)
        {

            int b = 1;
            int c = 0;
            for (int i = 0; i < a.Length; i++)
            {
                //Thread.Sleep(1);
                Console.SetCursorPosition(25 + b, 2 + c);

                if (a[i] != '.')
                {

                    if (a[i] == '█' || a[i] == '▄' || a[i] == '▀' || a[i] == '▐' || a[i] == '▌')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(a[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(a[i]);
                    }
                }

                if (a[i] == '.')
                {
                    b = 0;
                    c++;
                }

                b++;
            }
            int pos = 46;
            Console.SetCursorPosition(pos, 15);
            Console.Write("JUGAR");
            Console.SetCursorPosition(pos, 16);
            Console.Write("INSTRUCCIONES");
            Console.SetCursorPosition(pos, 17);
            Console.Write("RANKING");
            Console.SetCursorPosition(pos, 18);
            Console.Write("SALIR");

            Console.SetCursorPosition(pos - 1, 15);
            Console.Write((char)26);

        }
    }
}
