namespace SimuladorBanco.Entidades
{
    public class Cliente
    {
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroCuenta { get; set; }
        public double Saldo { get; set; }

        public Cliente(string cedula, string nombreCompleto, string numeroCuenta, double saldo)
        {
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            NumeroCuenta = numeroCuenta;
            Saldo = saldo;
        }

        public override string ToString()
        {
            return $"[{NumeroCuenta}] {NombreCompleto} | Cédula: {Cedula} | Saldo: ${Saldo:F2}";
        }
    }
}