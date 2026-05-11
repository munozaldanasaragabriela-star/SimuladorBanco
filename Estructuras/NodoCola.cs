namespace SimuladorBanco.Estructuras
{
    public class NodoCliente
    {
        public Entidades.Cliente Dato { get; set; }
        public NodoCliente Siguiente { get; set; }

        public NodoCliente(Entidades.Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }
}   