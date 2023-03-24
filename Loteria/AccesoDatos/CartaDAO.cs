using Loteria.BO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.AccesoDatos
{
    public class CartaDAO
    {
        private static readonly string _connectionString ="Server=<nombre_servidor>;Database=<nombre_base_datos>;User Id=<nombre_usuario>;Password=<contraseña>";

        public CartaDAO() { 
        } 

        public static void AgregaCarta (Carta carta)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO cartas (nombre_carta, imagen_carta) VALUES (@nombre_carta, @imagen_carta); SELECT SCOPE_IDENTITY();";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre_carta", carta.NombreCarta);
                    comando.Parameters.AddWithValue("@imagen_carta", carta.ImagenCarta);

                    conexion.Open();
                    carta.IdCarta = Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public static void ActualizarCarta(Carta carta)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "UPDATE cartas SET nombre_carta = @nombre_carta, imagen_carta = @imagen_carta WHERE id_carta = @id_carta";
                using (SqlCommand comando = new SqlCommand(query, conexion)) {
                    comando.Parameters.AddWithValue("@nombre_carta", carta.NombreCarta);
                    comando.Parameters.AddWithValue("@imagen_carta", carta.ImagenCarta);
                    comando.Parameters.AddWithValue("@id_carta", carta.IdCarta);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void EliminarCarta(int idCarta)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM cartas WHERE id_carta = @id_carta";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id_carta", idCarta);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public static Carta GetCartaById(int idCarta)
        {
            string query = "SELECT * FROM cartas WHERE id_carta = @id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", idCarta);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Carta carta = new Carta();
                            carta.IdCarta = (int)reader["id_carta"];
                            carta.NombreCarta = (string)reader["nombre_carta"];
                            carta.ImagenCarta = (string)reader["imagen_carta"];
                            return carta;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static List<Carta> ObtenerTodasLasCartas()
        {
            List<Carta> cartas = new List<Carta>();
            string query = "SELECT * FROM cartas";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carta carta = new Carta();
                            carta.IdCarta = (int)reader["id_carta"];
                            carta.NombreCarta = (string)reader["nombre_carta"];
                            carta.ImagenCarta = (string)reader["imagen_carta"];

                            cartas.Add(carta);
                        }
                    }
                }
            }

            return cartas;
        }


    }

}
