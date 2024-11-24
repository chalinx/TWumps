using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wunpus
{
    abstract class Agente : Cdimension
    {
        protected int identidad;
        public bool colision;
        protected ConsoleColor foregroundColor;
        protected ConsoleColor backgroundColor;
        protected char displayChar;
        protected string nombre;

        public Agente(int pos_x, int pos_y, int identidad, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayChar, string nombre)
            : base(pos_x, pos_y)
        {
            this.identidad = identidad;
            this.colision = true;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.displayChar = displayChar;
            this.nombre = nombre;
        }

        public ConsoleColor ForegroundColor => foregroundColor;
        public ConsoleColor BackgroundColor => backgroundColor;
        public char DisplayChar => displayChar;
        public string Nombre => nombre;

        public override void Mostrar()
        {
            if (colision)
            {
                Console.SetCursorPosition(pos_x, pos_y);
                Console.ForegroundColor = foregroundColor;
                Console.BackgroundColor = backgroundColor;
                Console.Write(displayChar);
            }
        }

        public virtual void dato()
        {
            Console.SetCursorPosition(pos_x, pos_y);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"{displayChar} {nombre}");
        }

        public int Identidad
        {
            get { return identidad; }
            set { identidad = value; }
        }
    }
    sealed class Wunpu : Agente
    {
        public Wunpu(int x, int y)
            : base(x, y, 3, ConsoleColor.DarkRed, ConsoleColor.Gray, (char)2, "Wunpu") { }
    }

    sealed class Agujero : Agente
    {
        public Agujero(int x, int y)
            : base(x, y, 6, ConsoleColor.Black, ConsoleColor.Gray, (char)15, "Agujero") { }
    }

    sealed class Hedor : Agente
    {
        public Hedor(int x, int y)
            : base(x, y, 7, ConsoleColor.DarkMagenta, ConsoleColor.Gray, (char)167, "Hedor") { }
    }

    sealed class Viento : Agente
    {
        public Viento(int x, int y)
            : base(x, y, 2, ConsoleColor.DarkBlue, ConsoleColor.Gray, (char)64, "Viento") { }
    }

    sealed class Oro : Agente
    {
        public Oro(int x, int y)
            : base(x, y, 4, ConsoleColor.DarkYellow, ConsoleColor.Gray, (char)36, "Oro") { }
    }

    sealed class Flecha : Agente
    {
        public Flecha(int x, int y)
            : base(x, y, 5, ConsoleColor.DarkGreen, ConsoleColor.Gray, (char)125, "Flecha") { }
    }


    class CAgentes
    {
        private List<Agente> agent;
        private bool nColision;
        private int capturar_pos;
        public CAgentes()
        {
            agent = new List<Agente>();
            nColision = true;
        }
        ~CAgentes()
        {
            agent.Clear();
        }

        public void Ver()
        {
            for (int i = 0; i < agent.Count; i++)
            {
                agent[i].Mostrar();

            }

        }
        public bool Ncolision
        {
            get { return nColision; }
            set { nColision = value; }
        }
        public void DibujarAgentes(Personaje per)
        {
            for (int i = 0; i < agent.Count; i++)
            {
                if (agent[i].colision && per.Rango(agent[i].Pos_X, agent[i].Pos_Y))
                {
                    agent[i].Mostrar();
                    nColision = false;
                    break;
                }
                nColision = true;

            }


        }
        public void AgregarEnemigos(int color, int x, int y)
        {
            Agente nuevoAgente = null;

            //  agente base según el color
            switch (color)
            {
                case 2: nuevoAgente = new Viento(x, y); break;
                case 3: nuevoAgente = new Wunpu(x, y); break;
                case 4: nuevoAgente = new Oro(x, y); break;
                case 5: nuevoAgente = new Flecha(x, y); break;
                case 6: nuevoAgente = new Agujero(x, y); break;
                case 7: nuevoAgente = new Hedor(x, y); break;
            }

            // Si el agente fue creado exitosamente
            if (nuevoAgente != null)
            {
                // decorrar agnte, rstaltado y icono
                if (nuevoAgente is Oro)
                {
                    nuevoAgente = new AgenteResaltado(nuevoAgente, ConsoleColor.DarkYellow); 
                    nuevoAgente = new AgenteConIconoExtra(nuevoAgente, '$');               
                }
                else if (nuevoAgente is Wunpu)
                {
                    nuevoAgente = new AgenteResaltado(nuevoAgente, ConsoleColor.DarkRed);   // Resaltar Wunpu
                    nuevoAgente = new AgenteConIconoExtra(nuevoAgente, '☻');                // Agregar ícono especial
                }
                else if (nuevoAgente is Flecha)
                {
                    nuevoAgente = new AgenteResaltado(nuevoAgente, ConsoleColor.DarkGreen); // Resaltar Flecha
                    nuevoAgente = new AgenteConIconoExtra(nuevoAgente, '}');                // Agregar ícono especial

                }
                else if (nuevoAgente is Hedor)
                {
                    nuevoAgente = new AgenteConIconoExtra(nuevoAgente, '§');                // Agregar ícono especial
                }

                agent.Add(nuevoAgente);
            }
        }

        public int Capturar_Pos
        {
            get { return capturar_pos; }
            set { capturar_pos = value; }
        }
        public List<Agente> Agent
        {
            get { return agent; }
            set { agent = value; }
        }
        public bool colision
        {
            get { return colision; }
            set { colision = value; }
        }




    }
}
