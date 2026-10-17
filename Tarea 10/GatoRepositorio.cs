using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Tarea_No._10
{
    public class GastoRepositorio
    {


        private const string CadenaConexion =
               @"Server=(localdb)\MSSQLLocalDB;" +
               @"Database=GastosDB;" +
               @"Trusted_Connection=True;" +
               @"TrustServerCertificate=True;";
        public void Insertar(Gasto g)
        {
            string sql = "INSERT INTO Gastos (Descripcion, Monto, Categoria)" +
                "VALUES (@descripcion, @monto, @categoria)";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", g.descripcion);
                comando.Parameters.AddWithValue("@monto", g.monto);
                comando.Parameters.AddWithValue("@categoria", g.categoria);

                conexion.Open();
                int Filas = comando.ExecuteNonQuery();
                Console.WriteLine(Filas == 1 ? "Gasto Ingresado" : "No Se Inserto");
            }
        }
        public List<Gasto> ListarGastos()
        {
            List<Gasto> lista = new List<Gasto>();
            string sql = "SELECT Id, Descripcion, Monto, Categoria " +
                "FROM Gastos ORDER BY Id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(new Gasto
                        {
                            id = lector.GetInt32(0),
                            descripcion = lector.GetString(1),
                            monto = lector.GetDecimal(2),
                            categoria = lector.GetString(3)
                        });
                    }
                }
            }
            return lista;

        }

    }
}