using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Loteria.AccesoDatos
{
    public class Connexion
    {
        private static string _connectionString;

        /// <summary>
        /// Constructor con parametros personalizados 
        /// </summary>
        /// <param name="servidor"></param>
        /// <param name="baseDatos"></param>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        public Connexion(string servidor, string baseDatos, string usuario, string password)
        {
            // Se construye el string de conexión a la base de datos
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = servidor,
                InitialCatalog = baseDatos,
                UserID = usuario,
                Password = password
            };
            _connectionString = builder.ConnectionString;
        }

        /// <summary>
        /// Constructor con parametros en codigo duro. 
        /// </summary>
        public Connexion()
        {
            // Se construye el string de conexión a la base de datos
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "",
                InitialCatalog = "",
                UserID = "",
                Password = ""
            };
            _connectionString = builder.ConnectionString;
        }

        public static SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(_connectionString);
            conexion.Open();
            return conexion;
        }

        public static void CerrarConexion(SqlConnection conexion)
        {
            conexion.Close();
        }




    }
}
