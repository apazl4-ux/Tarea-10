using System;
using System.IO;
namespace Tarea_No._10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string opcion;
            GastoRepositorio repo = new GastoRepositorio();

            do
            {
                opcion = Menu();

                switch (opcion)
                {
                    case "1":
                        AgregarEnSqlServer(repo);
                        break;
                    case "2":
                        ListarEnSqlServer(repo);
                        break;
                    case "3":
                        Console.WriteLine("Saliendo del Programa");
                        break;
                    default:
                        Console.WriteLine("Opcion No Valida");
                        break;
                }
            }
            while (opcion != "3");
        }

        public static string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Registro De Gastos");
            Console.WriteLine("Opcion 1: Registrar Gasto Con SQL");
            Console.WriteLine("Opcion 2: Listar Gastos Con SQL");
            Console.WriteLine("Opcion 3: Salir");
            Console.Write("Escoja una Opcion: ");
            return Console.ReadLine();
        }

        public static void AgregarEnSqlServer(GastoRepositorio repo)
        {
            Console.WriteLine();
            Console.Write("Descripcion: ");
            string descripcion = Console.ReadLine().Trim();
            Console.Write("Monto: ");
            bool montoOk = decimal.TryParse(Console.ReadLine(), out decimal monto);
            Console.Write("Categoria: ");
            string Categoria = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(Categoria) || !montoOk || monto <= 0)
            {
                Console.WriteLine("Datos Invalidos");
                return;
            }

            repo.Insertar(new Gasto { descripcion = descripcion, monto = monto, categoria = Categoria });

        }
        public static void ListarEnSqlServer(GastoRepositorio repo)
        {
            List<Gasto> lista = repo.ListarGastos();
            if (lista.Count == 0)
            {
                Console.WriteLine("No Hay Productos en SQL Server");
                return;
            }

            decimal TotalGastado = 0;
            foreach (Gasto g in lista)
            {
                Console.WriteLine(g);
                TotalGastado += g.monto;
            }
            Console.WriteLine($"Total Gastado: {TotalGastado}");
        }
    }
}
