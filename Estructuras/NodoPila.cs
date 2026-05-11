namespace SimuladorBanco.Estructuras
{
    public class NodoPila
    {
        public Entidades.Transaccion Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Entidades.Transaccion transaccion)
        {
            Dato = transaccion;
            Siguiente = null;
        }
    }
}