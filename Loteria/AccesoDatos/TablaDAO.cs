using Loteria.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Loteria.AccesoDatos
{
    public class TablaDAO
    {
        private static readonly string _connectionString = "Server=<nombre_servidor>;Database=<nombre_base_datos>;User Id=<nombre_usuario>;Password=<contraseña>";
        public static void InsertarTabla(Tabla tabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO tablas (nombre_tabla, creada_en) VALUES (@nombre_tabla, @creada_en); SELECT CAST(scope_identity() AS int)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre_tabla", tabla.NombreTabla);
                comando.Parameters.AddWithValue("@creada_en", tabla.CreadaEn);
                conexion.Open();
                tabla.IdTabla = (int)comando.ExecuteScalar();
            }
        }

        public static void ActualizarTabla(Tabla tabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "UPDATE tablas SET nombre_tabla = @nombre_tabla, creada_en = @creada_en WHERE id_tabla = @id_tabla";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre_tabla", tabla.NombreTabla);
                comando.Parameters.AddWithValue("@creada_en", tabla.CreadaEn);
                comando.Parameters.AddWithValue("@id_tabla", tabla.IdTabla);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static void EliminarTabla(int idTabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM tablas WHERE id_tabla = @id_tabla";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id_tabla", idTabla);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static List<Tabla> GetAllTablas()
        {
            List<Tabla> tablas = new List<Tabla>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id_tabla, nombre_tabla, creada_en FROM tablas;";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idTabla = reader.GetInt32(0);
                    string nombreTabla = reader.GetString(1);
                    DateTime creadaEn = reader.GetDateTime(2);

                    Tabla tabla = new Tabla(idTabla, nombreTabla, creadaEn);

                    // Obtener las cartas de la tabla
                    List<CartaTabla> cartasTabla = CartaTablaDAO.GetCartasTablaByTabla(idTabla);

                    // Agregar las cartas a la tabla
                    foreach (CartaTabla cartaTabla in cartasTabla)
                    {
                        CartaTablaDAO.InsertarCartaTabla(cartaTabla);
                    }

                    tablas.Add(tabla);
                }
            }

            return tablas;
        }

        public static Tabla GetTablaById(int idTabla)
        {
            string query = "SELECT * FROM tablas WHERE id_tabla = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idTabla);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Tabla tabla = new Tabla();
                            tabla.IdTabla = (int)reader["id_tabla"];
                            tabla.NombreTabla = (string)reader["nombre_tabla"];
                            tabla.CreadaEn = (DateTime)reader["creada_en"];
                            return tabla;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static bool VerificarTablaExistente(Tabla tabla)
        {
            // Obtener todas las tablas de la base de datos
            List<Tabla> tablas = GetAllTablas();

            // Verificar si alguna tabla es igual a la tabla a generar
            foreach (Tabla tablaExistente in tablas)
            {
                if (tabla.Equals(tablaExistente))
                {
                    return true;
                }
            }

            return false;
        }


    }
}
