using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wunpus.Aspects;

namespace Wunpus
{
    [NotNullAspect]
    abstract class Cdimension
    {
        protected int pos_x;
        protected int pos_y;
        protected Cdimension() { }

        protected Cdimension(int pos_x, int pos_y)
        {
            this.pos_x = pos_x;
            this.pos_y = pos_y;
        }

        virtual public void Mostrar() { }

        virtual public void Borrar() { }
        public int Pos_X
        {
            get
            {
                return pos_x;
            }
            set
            {
                pos_x = value;
            }
        }
        public int Pos_Y
        {
            get
            {
                return pos_y;
            }
            set
            {
                pos_y = value;
            }
        }
    }
}
