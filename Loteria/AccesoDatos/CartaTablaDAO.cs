using Loteria.BO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.AccesoDatos
{
    public class CartaTablaDAO
    {
        private static readonly string _connectionString = "Server=<nombre_servidor>;Database=<nombre_base_datos>;User Id=<nombre_usuario>;Password=<contraseña>";

        public static void InsertarCartaTabla(CartaTabla cartaTabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO carta_tabla (id_carta, id_tabla, posicion) VALUES (@id_carta, @id_tabla, @posicion)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id_carta", cartaTabla.IdCarta);
                comando.Parameters.AddWithValue("@id_tabla", cartaTabla.IdTabla);
                comando.Parameters.AddWithValue("@posicion", cartaTabla.Posicion);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static void ActualizarCartaTabla(CartaTabla cartaTabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "UPDATE carta_tabla SET posicion = @posicion WHERE id_carta = @id_carta AND id_tabla = @id_tabla";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@posicion", cartaTabla.Posicion);
                comando.Parameters.AddWithValue("@id_carta", cartaTabla.IdCarta);
                comando.Parameters.AddWithValue("@id_tabla", cartaTabla.IdTabla);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static void EliminarCartaTabla(int idCarta, int idTabla)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM carta_tabla WHERE id_carta = @id_carta AND id_tabla = @id_tabla";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id_carta", idCarta);
                comando.Parameters.AddWithValue("@id_tabla", idTabla);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static List<CartaTabla> GetCartasTablaByTabla(Tabla tabla)
        {
            List<CartaTabla> cartasTabla = new List<CartaTabla>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_carta, id_tabla, posicion FROM cartas_tabla WHERE id_tabla = @idTabla ORDER BY posicion ASC";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idTabla", tabla.IdTabla);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int idCarta = Convert.ToInt32(reader["id_carta"]);
                        int idTabla = Convert.ToInt32(reader["id_tabla"]);
                        int posicion = Convert.ToInt32(reader["posicion"]);

                        Carta carta = CartaDAO.GetCartaById(idCarta);

                        CartaTabla cartaTabla = new CartaTabla(carta, tabla, posicion);
                        cartasTabla.Add(cartaTabla);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener las cartas de la tabla: " + e.Message);
            }

            return cartasTabla;
        }
        public static List<CartaTabla> GetCartasTablaByTabla(int idTabla)
        {
            Tabla tabla = TablaDAO.GetTablaById(idTabla);
            return GetCartasTablaByTabla(tabla);
        }

    }



}
