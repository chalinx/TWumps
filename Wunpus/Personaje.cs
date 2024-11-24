using System;
using System.Collections.Generic;
using Wunpus.Aspects;


namespace Wunpus
{
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }

    sealed class Personaje : Cdimension
    {
        private int vida;
        private int score;
        private int cambio;
        private List<Flecha> Arco;
        private Escenario esc; 
        private Manejador manejadorPrincipal;


        private static readonly Dictionary<int, (ConsoleColor color, char caracter)> visualizacion = new Dictionary<int, (ConsoleColor, char)>
        {
            { Const.viento, (ConsoleColor.DarkBlue, (char)2) },
            { Const.oro, (ConsoleColor.DarkYellow, (char)2) },
            { Const.flecha, (ConsoleColor.DarkGreen, (char)2) },
            { Const.hedor, (ConsoleColor.DarkMagenta, (char)2) },
        };

        public Personaje(int x, int y) { pos_x = x; pos_y = y; }

        public Personaje(Escenario esc) : base()
        {
            this.esc = esc;
            Arco = new List<Flecha>();
            pos_x = esc.Pos_Y + 1;
            pos_y = esc.Pos_X + 1;
            vida = 3;
            cambio = 0;

            var manejadorFlecha = new ManejadorFlecha();
            var manejadorOro = new ManejadorOro();
            var manejadorColision = new ManejadorColision();
            var manejadorDefault = new ManejadorDefault();

            manejadorFlecha.EstablecerSiguiente(manejadorOro);
            manejadorOro.EstablecerSiguiente(manejadorColision);
            manejadorColision.EstablecerSiguiente(manejadorDefault);

            manejadorPrincipal = manejadorFlecha;

            Borrar();
            Mostrar();
            datos();
        }

        public void datos()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 4); Console.Write("Score:" + score);
            Console.SetCursorPosition(40, 3); Console.Write("Vida:");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(45, 3); Console.Write("   ");
            for (int i = 0; i < vida; i++)
            {
                Console.SetCursorPosition(45 + i, 3);
                Console.Write((char)3);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(49, 3);
            Console.Write("Flechas:" + Arco.Count);
        }

        /*private void InteraccionAgente(Agente agente, ConsoleKeyInfo tecla)
        {
            if (agente.Pos_X == pos_x && agente.Pos_Y == pos_y)
            {
                agente.Mostrar();
                if (tecla.Key == ConsoleKey.Enter)
                {
                    switch (agente.Identidad)
                    {
                        case Const.flecha:
                            Arco.Add(new Flecha(pos_x, pos_y));
                            break;
                        case Const.oro:
                            score += 10;
                            break;
                            // Otros casos si es necesario
                    }
                    datos();
                    cambio = 0;
                    // Remover el agente después de interactuar
                    // El agente será eliminado en el método Agregar_y_eliminar_Agentes
                }
                else if (agente.Identidad == Const.wunpu || agente.Identidad == Const.agujero)
                {
                    vida--;
                    datos();
                    pos_x = esc.Pos_Y + 1;
                    pos_y = esc.Pos_X + 1;
                    Mostrar();

                }
              Mostrar();
            }
        }*/
       private void InteraccionAgente(Agente agente, ConsoleKeyInfo tecla)
{
    if (agente.Pos_X == pos_x && agente.Pos_Y == pos_y)
    {
        agente.Mostrar();

        if (tecla.Key == ConsoleKey.Enter || tecla.Key == ConsoleKey.Spacebar)
        {
            // Manejar la interacción a través del manejador principal
            manejadorPrincipal.Manejar(this, agente);

            switch (agente.Identidad)
            {
                case Const.flecha:
                    Arco.Add(new Flecha(pos_x, pos_y));
                    datos(); 
                    break;

                case Const.oro:
                    score += 10;
                    datos(); 
                    break;
            }
        }
        else if (agente.Identidad == Const.wunpu || agente.Identidad == Const.agujero)
        {

            vida--;
            datos(); 

            pos_x = esc.Pos_Y + 1;
            pos_y = esc.Pos_X + 1;
            Mostrar(); // Mostramos al personaje en la nueva posición
        }

        // Mostrar siempre después de cualquier interacción
        Mostrar();
    }
}


        [LoggingAspect]
        public void Agregar_y_eliminar_Agentes(ref CAgentes agente, ConsoleKeyInfo tecla)
        {
            for (int i = agente.Agent.Count - 1; i >= 0; i--)
            {
                InteraccionAgente(agente.Agent[i], tecla);
                if (agente.Agent[i].Pos_X == pos_x && agente.Agent[i].Pos_Y == pos_y && tecla.Key == ConsoleKey.Enter)
                {
                    agente.Agent.RemoveAt(i);
                }
            }
        }

        private void DetectarAgenteEnDireccion(Direccion direccion, ref CAgentes agent)
        {
            int dx = 0, dy = 0;
            switch (direccion)
            {
                case Direccion.Arriba: dy = -1; break;
                case Direccion.Abajo: dy = 1; break;
                case Direccion.Izquierda: dx = -1; break;
                case Direccion.Derecha: dx = 1; break;
            }

            int targetX = pos_x + dx;
            int targetY = pos_y + dy;
            bool colision = true;

            for (int i = agent.Agent.Count - 1; i >= 0; i--)
            {
                if (agent.Agent[i].Pos_X == targetX && agent.Agent[i].Pos_Y == targetY)
                {
                    Console.SetCursorPosition(targetX, targetY);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                    if (agent.Agent[i].Identidad == Const.wunpu)
                    {
                        agent.Agent.RemoveAt(i);
                    }
                    else
                    {
                        agent.Agent[i].Mostrar();
                    }
                    Arco.RemoveAt(Arco.Count - 1);
                    colision = false;
                    break;
                }
            }

            if (colision && esc.Es_camino(targetX, targetY))
            {
                Console.SetCursorPosition(targetX, targetY);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(" ");
                Arco.RemoveAt(Arco.Count - 1);
            }

            datos();
            Borrar();
        }

        public void Detectar_agente(ref CAgentes agent, ConsoleKeyInfo tecla)
        {
            if (Arco.Count > 0)
            {
                if (tecla.Key == ConsoleKey.W)
                    DetectarAgenteEnDireccion(Direccion.Arriba, ref agent);
                else if (tecla.Key == ConsoleKey.S)
                    DetectarAgenteEnDireccion(Direccion.Abajo, ref agent);
                else if (tecla.Key == ConsoleKey.A)
                    DetectarAgenteEnDireccion(Direccion.Izquierda, ref agent);
                else if (tecla.Key == ConsoleKey.D)
                    DetectarAgenteEnDireccion(Direccion.Derecha, ref agent);
            }
        }

        public override void Mostrar()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write((char)2); // Figura del personaje
        }

        public void dato()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write((char)2 + " Aventurero");
        }

        public void Cambio(ref CAgentes agent, int x, int y)
        {
            cambio = 0;
            foreach (var agente in agent.Agent)
            {
                if (agente.Pos_X == x && agente.Pos_Y == y)
                {
                    cambio = agente.Identidad;
                    break;
                }
            }
        }

        [LoggingAspect]
        public void Movimiento(ref Escenario esc, ConsoleKeyInfo tecla, ref CAgentes Agent)
        {
            if (Agent.Ncolision) Borrar();

            if (tecla.Key == ConsoleKey.UpArrow && esc.Es_camino(pos_x, pos_y - 1)) pos_y--;
            else if (tecla.Key == ConsoleKey.DownArrow && esc.Es_camino(pos_x, pos_y + 1)) pos_y++;
            else if (tecla.Key == ConsoleKey.RightArrow && esc.Es_camino(pos_x + 1, pos_y)) pos_x++;
            else if (tecla.Key == ConsoleKey.LeftArrow && esc.Es_camino(pos_x - 1, pos_y)) pos_x--;

            Cambio(ref Agent, pos_x, pos_y);
            Agregar_y_eliminar_Agentes(ref Agent, tecla);
            Detectar_agente(ref Agent, tecla);
            Mostrar();
            if (!game_over()) Borrar();
        }

        public bool Rango(int x, int y) => pos_x == x && pos_y == y;

        public override void Borrar()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(" ");
        }

        public bool game_over() => vida > 0;
        public int Vida
        {
            get { return vida; }
            set { vida = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void AgregarFlecha(Flecha flecha)
        {
            Arco.Add(flecha);
        }
        public void ReiniciarPosicion()
        {
            pos_x = esc.Pos_Y + 1;
            pos_y = esc.Pos_X + 1;
            Mostrar();
        }




    }
}
