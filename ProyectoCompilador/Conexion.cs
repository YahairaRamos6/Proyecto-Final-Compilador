using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace ProyectoFinal
{
    class Conexion
    {
        public static DataTable Query(string query)
        {
            string server = "localhost\\SQLEXPRESS";
            string basedatos = "Compilador";

            string cadena = "Server=" + server + ";Database=" + basedatos + ";Trusted_Connection=True;";
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();

            SqlCommand com = new SqlCommand(query, conexion);
            SqlDataReader resultado = com.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(resultado);

            resultado.Close();
            conexion.Close();
            com.Dispose();
            return dt;
        }

        public static string Encriptar(string contra)
        {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(contra);
            byte[] inArray = SHA1.ComputeHash(vectoBytes);
            SHA1.Clear();
            return Convert.ToBase64String(inArray);
        }
    }
}
