using System;
using Wunpus.Aspects;

namespace Wunpus
{
    class Escenario : Cdimension
    {
        private readonly int fila;
        private readonly int columna;
        private int[,] map;

        public Escenario(int pos_x, int pos_y, int fila, int columna, string archivoMapa, ref CAgentes ene) : base(pos_x, pos_y)
        {
            this.fila = fila;
            this.columna = columna;
            this.pos_x = pos_x;
            this.pos_y = pos_y;

            map = CargarMapaDesdeArchivo(archivoMapa, fila, columna);

            Pintar_escenario(ref ene);
        }

        [ExceptionHandlingAspect]
        private int[,] CargarMapaDesdeArchivo(string archivoMapa, int filas, int columnas)
        {
            int[,] mapa = new int[filas, columnas];
            try
            {
                using (var reader = new StreamReader(archivoMapa))
                {
                    string linea;
                    int filaActual = 0;

                    while ((linea = reader.ReadLine()) != null && filaActual < filas)
                    {
                        var valores = linea.Split(' '); // Dividir los números usando espacios
                        for (int col = 0; col < columnas; col++)
                        {
                            mapa[filaActual, col] = int.Parse(valores[col]);
                        }
                        filaActual++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo del mapa: {ex.Message}");
            }

            return mapa;
        }

        [PerformanceAspect]
        public void Pintar_escenario(ref CAgentes q)
        {
            for (int i = pos_x; i < fila + pos_x; i++)
            {
                for (int j = pos_y; j < columna + pos_y; j++)
                {
                    if (i == pos_x || j == pos_y || i == fila + pos_x - 1 || j == columna + pos_y - 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write((char)15);
                    }
                    else
                    {
                        int color = map[i - pos_x, j - pos_y];
                        q.AgregarEnemigos(color, j, i);
                        Console.SetCursorPosition(j, i);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public bool Es_camino(int x, int y)
        {
            return x >= pos_y + 1 && y >= pos_x + 1 && x < pos_y + columna - 1 && y < fila + pos_x - 1;
        }

        public int[,] Map
        {
            get { return map; }
            set { map = value; }
        }
    }
}