using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras
{
    public class NodoCola
    {
        public Cliente Dato { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }
}