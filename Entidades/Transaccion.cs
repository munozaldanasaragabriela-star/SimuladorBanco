namespace SimuladorBanco.Entidades
{
    public class Transaccion
    {
        public string NumeroCuenta;
        public string Tipo;
        public double Monto; 

        public Transaccion(string NumeroCuenta, string Tipo, double Monto)
        {
            NumeroCuenta= numerocuenta;
            Tipo= tipo;
            Monto= monto; 
        }
    }
}