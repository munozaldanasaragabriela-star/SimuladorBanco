using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras
{
    public class ColaAtencion
    {
        private NodoCola frente;
        private NodoCola final;
        private int cantidad;

        public ColaAtencion()
        {
            frente = null;
            final = null;
            cantidad = 0;
        }

        public void Encolar(Cliente cliente)
        {
            NodoCola nuevo = new NodoCola(cliente);

            if (final == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }

            cantidad++;
        }

        public Cliente Desencolar()
        {
            if (frente == null) return null;

            Cliente atendido = frente.Dato;
            frente = frente.Siguiente;

            if (frente == null) final = null;

            cantidad--;
            return atendido;
        }

        public Cliente VerSiguiente() => frente?.Dato;

        public void MostrarCola()
        {
            if (frente == null)
            {
                Console.WriteLine("  La cola de atención está vacía.");
                return;
            }

            NodoCola actual = frente;
            int turno = 1;
            while (actual != null)
            {
                Console.WriteLine($"  Turno {turno}: {actual.Dato.NombreCompleto} (Cuenta: {actual.Dato.NumeroCuenta})");
                actual = actual.Siguiente;
                turno++;
            }
        }

        public bool EstaVacia() => frente == null;
        public int Contar() => cantidad;
    }
}