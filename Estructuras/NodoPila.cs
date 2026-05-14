using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras
{
    public class NodoPila
    {
        public Transaccion Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Transaccion transaccion)
        {
            Dato = transaccion;
            Siguiente = null;
        }
    }
}