using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Media;

namespace Wunpus
{
    class Program
    {

        static void Main(string[] args)
        {
            // Cargar texto del archivo
            string q = GameUI.CargarTextoDesdeArchivo("principal.txt");

            if (string.IsNullOrEmpty(q))
            {
                Console.WriteLine("Error: El archivo de texto para el menú principal no se pudo cargar.");
                return;
            }

            // Mostrar el menú
            Menu(q);

            // Console.Write("↑");
        }

        static public void Menu(string a)
        {
            ConsoleKeyInfo tecla;

            Console.CursorVisible = false;
            int pos = 46;
            int opc = 0;
            GameUI.intro(a);

            do
            {
                tecla = Console.ReadKey(true);
                Console.SetCursorPosition(pos - 1, 15 + opc);
                Console.Write(" ");

                if (tecla.Key == ConsoleKey.UpArrow) opc--;
                else if (tecla.Key == ConsoleKey.DownArrow) opc++;
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (opc)
                    {
                        case 0: Game(); break;
                        case 1: GameUI.instrucciones(); break;
                        case 2: Console.Write("3"); break;
                        case 3: return;
                    }
                    Console.BackgroundColor = ConsoleColor.Black; Console.Clear();

                    GameUI.intro(a);
                    opc = 0;

                }
                if (opc < 0) opc = 3;
                else if (opc > 3) opc = 0;
                Console.SetCursorPosition(pos - 1, 15 + opc);
                Console.Write((char)26);
            } while (true);


        }
        static public bool Game()
        {
            ConsoleKeyInfo key;
            Console.CursorVisible = false;
            CAgentes ene = new CAgentes();
            string archivoMapa = "mapa.txt";
            // vertical,horizontal,columna,fila
            Escenario esc = new Escenario(5, 40, 20, 40, archivoMapa, ref ene);
            Personaje per = new Personaje(esc);
            // ene.Ver();
            GameUI.Jugabilidad();
            while (per.game_over())
            {
                key = Console.ReadKey(true);
                ene.DibujarAgentes(per);
                per.Movimiento(ref esc, key, ref ene);
                if (key.Key == ConsoleKey.Escape) break;
            }

            return false;
        }




    }


}







