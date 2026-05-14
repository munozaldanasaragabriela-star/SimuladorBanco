namespace SimuladorBanco.Entidades
{
    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    public class Transaccion
    {
        public string NumeroCuenta { get; set; }
        public TipoTransaccion Tipo { get; set; }
        public double Monto { get; set; }
        public double SaldoAnterior { get; set; }

        public Transaccion(string numeroCuenta, TipoTransaccion tipo, double monto, double saldoAnterior)
        {
            NumeroCuenta = numeroCuenta;
            Tipo = tipo;
            Monto = monto;
            SaldoAnterior = saldoAnterior;
        }

        public override string ToString()
        {
            string tipoStr = Tipo == TipoTransaccion.Deposito ? "Depósito" : "Retiro";
            return $"{tipoStr} de ${Monto:F2} en cuenta {NumeroCuenta} (saldo anterior: ${SaldoAnterior:F2})";
        }
    }
}