using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wunpus
{
    abstract class AgenteDecorator : Agente
    {
        protected Agente agente;
        public AgenteDecorator(Agente agente)
            : base(agente.Pos_X, agente.Pos_Y, agente.Identidad, agente.ForegroundColor, agente.BackgroundColor, agente.DisplayChar, agente.Nombre)
        {
            this.agente = agente;
        }

        public override void Mostrar()
        {
            agente.Mostrar();
        }

        public override void dato()
        {
            agente.dato();
        }
    }

    class AgenteResaltado : AgenteDecorator
    {
        private ConsoleColor colorResaltado;

        public AgenteResaltado(Agente agente, ConsoleColor colorResaltado)
            : base(agente)
        {
            this.colorResaltado = colorResaltado;
        }

        public override void Mostrar()
        {
            Console.BackgroundColor = colorResaltado;
            base.Mostrar();
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }

    class AgenteConIconoExtra : AgenteDecorator
    {
        private char iconoExtra;

        public AgenteConIconoExtra(Agente agente, char iconoExtra)
            : base(agente)
        {
            this.iconoExtra = iconoExtra;
        }

        public override void Mostrar()
        {
            base.Mostrar();
            Console.SetCursorPosition(agente.Pos_X+1, agente.Pos_Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(iconoExtra);
        }
    }

}
