using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras
{
    public class PilaTransacciones
    {
        private NodoPila tope;
        private int cantidad;

        public PilaTransacciones()
        {
            tope = null;
            cantidad = 0;
        }

        public void Apilar(Transaccion transaccion)
        {
            NodoPila nuevo = new NodoPila(transaccion);
            nuevo.Siguiente = tope;
            tope = nuevo;
            cantidad++;
        }

        public Transaccion Desapilar()
        {
            if (tope == null) return null;
            Transaccion ultima = tope.Dato;
            tope = tope.Siguiente;
            cantidad--;
            return ultima;
        }

        public Transaccion VerUltima() => tope?.Dato;
        public bool EstaVacia() => tope == null;
        public int Contar() => cantidad;
    }
}