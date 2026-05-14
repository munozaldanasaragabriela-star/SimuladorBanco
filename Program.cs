using SimuladorBanco.Logica;
using SimuladorBanco.Interfaz;

Banco banco = new Banco("SIMULADOR BANCO");
Menu menu = new Menu(banco);
menu.Ejecutar();