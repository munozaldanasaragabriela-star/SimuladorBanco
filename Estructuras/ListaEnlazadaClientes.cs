using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras
{
    public class ListaEnlazadaClientes
    {
        private NodoCliente cabeza;
        private int cantidad;

        public ListaEnlazadaClientes()
        {
            cabeza = null;
            cantidad = 0;
        }

        public bool Insertar(Cliente cliente)
        {
            if (BuscarPorCedula(cliente.Cedula) != null) return false;
            if (BuscarPorCuenta(cliente.NumeroCuenta) != null) return false;

            NodoCliente nuevo = new NodoCliente(cliente);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoCliente actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            cantidad++;
            return true;
        }

        public Cliente BuscarPorCedula(string cedula)
        {
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Cedula == cedula) return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        public Cliente BuscarPorCuenta(string numeroCuenta)
        {
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.NumeroCuenta == numeroCuenta) return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        public void ListarTodos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("  No hay clientes registrados.");
                return;
            }

            NodoCliente actual = cabeza;
            int i = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {i}. {actual.Dato}");
                actual = actual.Siguiente;
                i++;
            }
        }

        public int Contar() => cantidad;

        public double TotalDinero()
        {
            double total = 0;
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                total += actual.Dato.Saldo;
                actual = actual.Siguiente;
            }
            return total;
        }

        public bool EstaVacia() => cabeza == null;
    }
}