namespace Wunpus
{
    abstract class Manejador
    {
        protected Manejador siguiente;

        public void EstablecerSiguiente(Manejador manejador)
        {
            siguiente = manejador;
        }

        public abstract void Manejar(Personaje personaje, Agente agente);
    }

    class ManejadorFlecha : Manejador
    {
        public override void Manejar(Personaje personaje, Agente agente)
        {
            if (agente is Flecha)
            {
                personaje.AgregarFlecha(new Flecha(personaje.Pos_X, personaje.Pos_Y));
                personaje.datos();
                agente.colision = false;
            }
            else
            {
                siguiente?.Manejar(personaje, agente);
            }
        }
    }

    class ManejadorOro : Manejador
    {
        public override void Manejar(Personaje personaje, Agente agente)
        {
            if (agente is Oro)
            {
                personaje.Score += 10;
                personaje.datos();
                agente.colision = false;
            }
            else
            {
                siguiente?.Manejar(personaje, agente);
            }
        }
    }

    class ManejadorColision : Manejador
    {
        public override void Manejar(Personaje personaje, Agente agente)
        {
            if (agente is Wunpu || agente is Agujero)
            {
                personaje.Vida--;
                personaje.datos();
                personaje.ReiniciarPosicion();
            }
            else
            {
                siguiente?.Manejar(personaje, agente);
            }
        }
    }

    class ManejadorDefault : Manejador
    {
        public override void Manejar(Personaje personaje, Agente agente)
        {
        }
    }
}
