using SimuladorBanco.Logica;

namespace SimuladorBanco.Interfaz
{
    public class Menu
    {
        private Banco banco;

        public Menu(Banco banco)
        {
            this.banco = banco;
        }

        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine()?.Trim();

                switch (opcion)
                {
                    case "1":  RegistrarCliente();     break;
                    case "2":  ListarClientes();       break;
                    case "3":  BuscarCliente();        break;
                    case "4":  AgregarACola();         break;
                    case "5":  AtenderSiguiente();     break;
                    case "6":  RealizarDeposito();     break;
                    case "7":  RealizarRetiro();       break;
                    case "8":  ConsultarSaldo();       break;
                    case "9":  DeshacerTransaccion();  break;
                    case "10": MostrarCola();          break;
                    case "11": MostrarTotalClientes(); break;
                    case "12": MostrarTotalDinero();   break;
                    case "13": salir = true;           break;
                    default:
                        Alerta("Opción inválida. Ingrese un número del 1 al 13.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\n  Presione Enter para continuar...");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  Gracias por usar el simulador bancario. ¡Hasta pronto!\n");
            Console.ResetColor();
        }

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  {banco.Nombre,-44}");
            Console.ResetColor();

            string[] opciones = {
                " 1. Registrar cliente",
                " 2. Listar clientes",
                " 3. Buscar cliente",
                " 4. Agregar cliente a cola de atención",
                " 5. Atender siguiente cliente",
                " 6. Realizar depósito",
                " 7. Realizar retiro",
                " 8. Consultar saldo",
                " 9. Deshacer última transacción",
                "10. Mostrar cola de atención",
                "11. Mostrar total de clientes",
                "12. Mostrar total de dinero del banco",
                "13. Salir"
            };

            foreach (string op in opciones)
                Console.WriteLine($"║  {op,-44}║");

            Console.ForegroundColor = ConsoleColor.Cyan;    
            Console.ResetColor();
            Console.Write("\n  Seleccione una opción: ");
        }

        private void RegistrarCliente()
        {
            Titulo("REGISTRAR CLIENTE");

            Console.Write("  Cédula: ");
            string cedula = Console.ReadLine()?.Trim();

            Console.Write("  Nombre completo: ");
            string nombre = Console.ReadLine()?.Trim();

            Console.Write("  Número de cuenta: ");
            string cuenta = Console.ReadLine()?.Trim();

            Console.Write("  Saldo inicial: $");
            if (!double.TryParse(Console.ReadLine(), out double saldo) || saldo < 0)
            {
                Alerta("Saldo inválido.");
                return;
            }

            if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(cuenta))
            {
                Alerta("Todos los campos son obligatorios.");
                return;
            }

            if (banco.RegistrarCliente(cedula, nombre, cuenta, saldo))
                Exito($"Cliente '{nombre}' registrado correctamente.");
            else
                Alerta("Ya existe un cliente con esa cédula o número de cuenta.");
        }

        private void ListarClientes()
        {
            Titulo("LISTA DE CLIENTES");
            banco.ListarClientes();
        }

        private void BuscarCliente()
        {
            Titulo("BUSCAR CLIENTE");
            Console.Write("  Ingrese cédula o número de cuenta: ");
            string valor = Console.ReadLine()?.Trim();

            var cliente = banco.BuscarPorCedula(valor) ?? banco.BuscarPorCuenta(valor);

            if (cliente != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  ✔ Cliente encontrado:\n    {cliente}");
                Console.ResetColor();
            }
            else
            {
                Alerta("No se encontró ningún cliente con ese dato.");
            }
        }

        private void AgregarACola()
        {
            Titulo("AGREGAR A COLA DE ATENCIÓN");
            Console.Write("  Cédula del cliente: ");
            string cedula = Console.ReadLine()?.Trim();

            if (banco.AgregarACola(cedula))
                Exito("Cliente agregado a la cola de atención.");
            else
                Alerta("No se encontró un cliente con esa cédula.");
        }

        private void AtenderSiguiente()
        {
            Titulo("ATENDER SIGUIENTE CLIENTE");

            if (banco.ColaVacia())
            {
                Alerta("No hay clientes en la cola de atención.");
                return;
            }

            var cliente = banco.AtenderSiguiente();
            Exito($"Atendiendo a: {cliente!.NombreCompleto} | Cuenta: {cliente.NumeroCuenta}");
        }

        private void RealizarDeposito()
        {
            Titulo("REALIZAR DEPÓSITO");
            Console.Write("  Número de cuenta: ");
            string cuenta = Console.ReadLine()?.Trim();

            Console.Write("  Monto a depositar: $");
            if (!double.TryParse(Console.ReadLine(), out double monto) || monto <= 0)
            {
                Alerta("Monto inválido.");
                return;
            }

            if (banco.Depositar(cuenta, monto))
                Exito($"Depósito de ${monto:F2} realizado. Nuevo saldo: ${banco.ConsultarSaldo(cuenta):F2}");
            else
                Alerta("Cuenta no encontrada o monto inválido.");
        }

        private void RealizarRetiro()
        {
            Titulo("REALIZAR RETIRO");
            Console.Write("  Número de cuenta: ");
            string cuenta = Console.ReadLine()?.Trim();

            Console.Write("  Monto a retirar: $");
            if (!double.TryParse(Console.ReadLine(), out double monto) || monto <= 0)
            {
                Alerta("Monto inválido.");
                return;
            }

            if (banco.Retirar(cuenta, monto))
                Exito($"Retiro de ${monto:F2} realizado. Nuevo saldo: ${banco.ConsultarSaldo(cuenta):F2}");
            else
                Alerta("Cuenta no encontrada, saldo insuficiente o monto inválido.");
        }

        private void ConsultarSaldo()
        {
            Titulo("CONSULTAR SALDO");
            Console.Write("  Número de cuenta: ");
            string cuenta = Console.ReadLine()?.Trim();

            double saldo = banco.ConsultarSaldo(cuenta);

            if (saldo >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  Saldo de la cuenta {cuenta}: ${saldo:F2}");
                Console.ResetColor();
            }
            else
            {
                Alerta("Cuenta no encontrada.");
            }
        }

        private void DeshacerTransaccion()
        {
            Titulo("DESHACER ÚLTIMA TRANSACCIÓN");

            if (!banco.HayTransacciones())
            {
                Alerta("No hay transacciones para deshacer.");
                return;
            }

            string resultado = banco.DeshacerUltima();
            Exito(resultado!);
        }

        private void MostrarCola()
        {
            Titulo("COLA DE ATENCIÓN ACTUAL");
            banco.MostrarCola();
        }

        private void MostrarTotalClientes()
        {
            Titulo("TOTAL DE CLIENTES");
            Console.WriteLine($"  Clientes registrados: {banco.TotalClientes()}");
        }

        private void MostrarTotalDinero()
        {
            Titulo("TOTAL DE DINERO EN EL BANCO");
            Console.WriteLine($"  Dinero total en todas las cuentas: ${banco.TotalDinero():F2}");
        }

        private void Titulo(string texto)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  ── {texto} ──\n");
            Console.ResetColor();
        }

        private void Exito(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✔ {msg}");
            Console.ResetColor();
        }

        private void Alerta(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ✘ {msg}");
            Console.ResetColor();
        }
    }
}