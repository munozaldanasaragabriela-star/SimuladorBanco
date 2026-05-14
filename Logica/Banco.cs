using SimuladorBanco.Entidades;
using SimuladorBanco.Estructuras;

namespace SimuladorBanco.Logica
{
    public class Banco
    {
        private ListaEnlazadaClientes listaClientes;
        private ColaAtencion colaAtencion;
        private PilaTransacciones pilaTransacciones;

        public string Nombre { get; private set; }

        public Banco(string nombre)
        {
            Nombre = nombre;
            listaClientes = new ListaEnlazadaClientes();
            colaAtencion = new ColaAtencion();
            pilaTransacciones = new PilaTransacciones();
        }

        public bool RegistrarCliente(string cedula, string nombre, string numeroCuenta, double saldo)
        {
            Cliente nuevo = new Cliente(cedula, nombre, numeroCuenta, saldo);
            return listaClientes.Insertar(nuevo);
        }

        public Cliente BuscarPorCedula(string cedula) => listaClientes.BuscarPorCedula(cedula);
        public Cliente BuscarPorCuenta(string cuenta) => listaClientes.BuscarPorCuenta(cuenta);
        public void ListarClientes() => listaClientes.ListarTodos();
        public int TotalClientes() => listaClientes.Contar();
        public double TotalDinero() => listaClientes.TotalDinero();

        public bool AgregarACola(string cedula)
        {
            Cliente c = listaClientes.BuscarPorCedula(cedula);
            if (c == null) return false;
            colaAtencion.Encolar(c);
            return true;
        }

        public Cliente AtenderSiguiente() => colaAtencion.Desencolar();
        public void MostrarCola() => colaAtencion.MostrarCola();
        public bool ColaVacia() => colaAtencion.EstaVacia();

        public bool Depositar(string numeroCuenta, double monto)
        {
            Cliente c = listaClientes.BuscarPorCuenta(numeroCuenta);
            if (c == null || monto <= 0) return false;

            double anterior = c.Saldo;
            c.Saldo += monto;
            pilaTransacciones.Apilar(new Transaccion(numeroCuenta, TipoTransaccion.Deposito, monto, anterior));
            return true;
        }

        public bool Retirar(string numeroCuenta, double monto)
        {
            Cliente c = listaClientes.BuscarPorCuenta(numeroCuenta);
            if (c == null || monto <= 0 || c.Saldo < monto) return false;

            double anterior = c.Saldo;
            c.Saldo -= monto;
            pilaTransacciones.Apilar(new Transaccion(numeroCuenta, TipoTransaccion.Retiro, monto, anterior));
            return true;
        }

        public double ConsultarSaldo(string numeroCuenta)
        {
            Cliente c = listaClientes.BuscarPorCuenta(numeroCuenta);
            return c != null ? c.Saldo : -1;
        }

        public string DeshacerUltima()
        {
            if (pilaTransacciones.EstaVacia()) return null;

            Transaccion t = pilaTransacciones.Desapilar();
            Cliente c = listaClientes.BuscarPorCuenta(t.NumeroCuenta);
            if (c == null) return "Error: cliente no encontrado.";

            c.Saldo = t.SaldoAnterior;
            return $"Revertida: {t}";
        }

        public bool HayTransacciones() => !pilaTransacciones.EstaVacia();
    }
}